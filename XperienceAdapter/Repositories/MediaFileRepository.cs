﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.Membership;
using Core.Configuration;
using Kentico.Content.Web.Mvc;
using Microsoft.Extensions.Options;
using XperienceAdapter.Models;

namespace XperienceAdapter.Repositories
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly IMediaLibraryInfoProvider _mediaLibraryInfoProvider;

        private readonly IMediaFileInfoProvider _mediaFileInfoProvider;

        private readonly ISiteService _siteService;

        private readonly IMediaFileUrlRetriever _mediaFileUrlRetriever;

        private readonly IOptionsMonitor<XperienceOptions> _optionsMonitor;

        public MediaFileRepository(
            IMediaLibraryInfoProvider mediaLibraryInfoProvider, 
            IMediaFileInfoProvider mediaFileInfoProvider, 
            ISiteService siteService, 
            IMediaFileUrlRetriever mediaFileUrlRetriever,
            IOptionsMonitor<XperienceOptions> optionsMonitor)
        {
            _mediaLibraryInfoProvider = mediaLibraryInfoProvider ?? throw new ArgumentNullException(nameof(mediaLibraryInfoProvider));
            _mediaFileInfoProvider = mediaFileInfoProvider ?? throw new ArgumentNullException(nameof(mediaFileInfoProvider));
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
            _mediaFileUrlRetriever = mediaFileUrlRetriever ?? throw new ArgumentNullException(nameof(mediaFileUrlRetriever));
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
        }

        public async Task<Guid> AddMediaFileAsync(string filePath, string mediaLibraryName, string? libraryFolderPath = default, bool checkPermissions = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path was not specified.", nameof(filePath));
            }

            return await AddMediaFileAsyncImplementation();

            async Task<Guid> AddMediaFileAsyncImplementation()
            {
                var siteId = _siteService.CurrentSite.SiteID;
                var siteName = _siteService.CurrentSite.SiteName;
                MediaLibraryInfo mediaLibraryInfo;

                try
                {
                    mediaLibraryInfo = await _mediaLibraryInfoProvider.GetAsync(mediaLibraryName, siteId);
                }
                catch (Exception)
                {
                    throw new Exception($"The {mediaLibraryName} library was not found on the {siteName} site.");
                }

                if (checkPermissions && !mediaLibraryInfo.CheckPermissions(PermissionsEnum.Create, siteName, MembershipContext.AuthenticatedUser))
                {
                    throw new PermissionException(
                        $"The user {MembershipContext.AuthenticatedUser.FullName} lacks permissions to the {mediaLibraryName} library.");
                }

                MediaFileInfo mediaFile = !string.IsNullOrEmpty(libraryFolderPath)
                    ? new MediaFileInfo(filePath, mediaLibraryInfo.LibraryID, libraryFolderPath)
                    : new MediaFileInfo(filePath, mediaLibraryInfo.LibraryID);

                var fileInfo = FileInfo.New(filePath);
                mediaFile.FileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
                mediaFile.FileExtension = fileInfo.Extension;
                mediaFile.FileMimeType = MimeTypeHelper.GetMimetype(fileInfo.Extension);
                mediaFile.FileSiteID = siteId;
                mediaFile.FileLibraryID = mediaLibraryInfo.LibraryID;
                mediaFile.FileSize = fileInfo.Length;
                _mediaFileInfoProvider.Set(mediaFile);

                return mediaFile.FileGUID;
            }
        }

        public async Task<MediaLibraryFile?> GetMediaFileAsync(Guid fileGuid, CancellationToken? cancellationToken = default)
        {
            var mediaFileInfo = await _mediaFileInfoProvider.GetAsync(fileGuid, _siteService.CurrentSite.SiteID, cancellationToken);

            return mediaFileInfo != null ? MapDtoProperties(mediaFileInfo) : null;
        }

        public async Task<MediaLibraryFile?> GetMediaFileAsync(string fileGuid, CancellationToken? cancellationToken = default)
        {
            var guid = ValidationHelper.GetGuid(fileGuid, Guid.Empty);

            var mediaFileInfo = await _mediaFileInfoProvider.GetAsync(guid, _siteService.CurrentSite.SiteID, cancellationToken);

            return mediaFileInfo != null ? MapDtoProperties(mediaFileInfo) : null;
        }

        public async Task<MediaLibraryFile> GetMediaFileAsync(string mediaLibraryName, string path, CancellationToken? cancellationToken = default)
        {
            var libraryId = GetLibraryId(mediaLibraryName);

            return (await GetResultAsync(baseQuery => 
                baseQuery
                    .WhereEquals("FileLibraryID", libraryId)
                    .WhereStartsWith("FilePath", path), 
                cancellationToken))
                .FirstOrDefault();
        }

        public async Task<IEnumerable<MediaLibraryFile>> GetMediaFilesAsync(CancellationToken? cancellationToken = default, params Guid[] fileGuids) =>
            await GetResultAsync(baseQuery =>
                    baseQuery
                    .WhereIn("FileGUID", fileGuids),
                cancellationToken);

        public async Task<IEnumerable<MediaLibraryFile>> GetMediaFilesAsync(string mediaLibraryName, CancellationToken? cancellationToken = default, params string[] extensions)
        {
            var libraryId = GetLibraryId(mediaLibraryName);

            return await GetResultAsync(baseQuery =>
                baseQuery
                    .WhereIn("FileExtension", extensions),
                cancellationToken);
        }

        public async Task<IEnumerable<MediaLibraryFile>> GetMediaFilesAsync(string mediaLibraryName, string path, CancellationToken? cancellationToken = default)
        {
            var libraryId = GetLibraryId(mediaLibraryName);

            return await GetResultAsync(baseQuery =>
                baseQuery
                    .WhereStartsWith("FilePath", path),
                cancellationToken);
        }

        public async Task<IEnumerable<MediaLibraryFile>> GetAllAsync(CancellationToken? cancellationToken = default) =>
            await GetResultAsync(null, cancellationToken: cancellationToken);

        public IEnumerable<MediaLibraryFile> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Gets a media library ID by its code name.
        /// </summary>
        /// <param name="mediaLibraryName">Code name.</param>
        /// <returns>Library ID.</returns>
        private int GetLibraryId(string mediaLibraryName)
        {
            if (string.IsNullOrEmpty(mediaLibraryName))
            {
                throw new ArgumentException($"The {nameof(mediaLibraryName)} parameter must a non-empty string.");
            }

            return _mediaLibraryInfoProvider
                .Get(mediaLibraryName, _siteService.CurrentSite.SiteID)
                .LibraryID;
        }

        /// <summary>
        /// Gets a query with an optional filter.
        /// </summary>
        /// <param name="filter">Optional filter.</param>
        /// <returns></returns>
        private async Task<IEnumerable<MediaLibraryFile>> GetResultAsync(
            Func<ObjectQuery<MediaFileInfo>, ObjectQuery<MediaFileInfo>>? filter,
            CancellationToken? cancellationToken)
        {
            var query = _mediaFileInfoProvider.Get();

            if (filter != null)
            {
                query = filter(query);
            }

            return (await query.GetEnumerableTypedResultAsync(cancellationToken: cancellationToken))
                .Select(item => MapDtoProperties(item));
        }

        /// <summary>
        /// Maps DTO properties.
        /// </summary>
        /// <param name="mediaFileInfo">Xperience media file.</param>
        /// <returns>Media file DTO.</returns>
        private MediaLibraryFile MapDtoProperties(MediaFileInfo mediaFileInfo) =>
            new MediaLibraryFile()
            {
                Guid = mediaFileInfo.FileGUID,
                Name = mediaFileInfo.FileTitle,
                Extension = mediaFileInfo.FileExtension,
                MediaFileUrl = _mediaFileUrlRetriever.Retrieve(mediaFileInfo),
                IsImage = _optionsMonitor.CurrentValue?.MediaLibraryOptions?.AllowedImageExtensions?.Contains(mediaFileInfo.FileExtension) == true,
                Width = mediaFileInfo.FileImageWidth,
                Height = mediaFileInfo.FileImageHeight
            };
    }
}
