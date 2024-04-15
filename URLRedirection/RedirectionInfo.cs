using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using URLRedirection;

[assembly: RegisterObjectType(typeof(RedirectionInfo), RedirectionInfo.OBJECT_TYPE)]

namespace URLRedirection
{
    /// <summary>
    /// Data container class for <see cref="RedirectionInfo"/>.
    /// </summary>
    [Serializable]
    public partial class RedirectionInfo : AbstractInfo<RedirectionInfo, IRedirectionInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "urlredirection.redirection";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(RedirectionInfoProvider), OBJECT_TYPE, "URLRedirection.Redirection", "RedirectionID", "RedirectionLastModified", "RedirectionGuid", null, "RedirectionOriginalUrl", null, "RedirectionSiteID", null, null)
        {
            ModuleName = "URLRedirection",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Redirection ID.
        /// </summary>
        [DatabaseField]
        public virtual int RedirectionID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("RedirectionID"), 0);
            }
            set
            {
                SetValue("RedirectionID", value);
            }
        }


        /// <summary>
        /// Redirection original url.
        /// </summary>
        [DatabaseField]
        public virtual string RedirectionOriginalUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("RedirectionOriginalUrl"), String.Empty);
            }
            set
            {
                SetValue("RedirectionOriginalUrl", value);
            }
        }


        /// <summary>
        /// Redirection target url.
        /// </summary>
        [DatabaseField]
        public virtual string RedirectionTargetUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("RedirectionTargetUrl"), String.Empty);
            }
            set
            {
                SetValue("RedirectionTargetUrl", value);
            }
        }


        /// <summary>
        /// Redirection comment.
        /// </summary>
        [DatabaseField]
        public virtual string RedirectionComment
        {
            get
            {
                return ValidationHelper.GetString(GetValue("RedirectionComment"), String.Empty);
            }
            set
            {
                SetValue("RedirectionComment", value, String.Empty);
            }
        }


        /// <summary>
        /// Redirection site ID.
        /// </summary>
        [DatabaseField]
        public virtual int RedirectionSiteID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("RedirectionSiteID"), 0);
            }
            set
            {
                SetValue("RedirectionSiteID", value);
            }
        }


        /// <summary>
        /// Redirection type.
        /// </summary>
        [DatabaseField]
        public virtual string RedirectionType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("RedirectionType"), String.Empty);
            }
            set
            {
                SetValue("RedirectionType", value);
            }
        }


        /// <summary>
        /// Redirection guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid RedirectionGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("RedirectionGuid"), Guid.Empty);
            }
            set
            {
                SetValue("RedirectionGuid", value);
            }
        }


        /// <summary>
        /// Redirection last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime RedirectionLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("RedirectionLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("RedirectionLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected RedirectionInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="RedirectionInfo"/> class.
        /// </summary>
        public RedirectionInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="RedirectionInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public RedirectionInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}