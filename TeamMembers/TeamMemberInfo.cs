using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using TeamMembers;

[assembly: RegisterObjectType(typeof(TeamMemberInfo), TeamMemberInfo.OBJECT_TYPE)]

namespace TeamMembers
{
    /// <summary>
    /// Data container class for <see cref="TeamMemberInfo"/>.
    /// </summary>
    [Serializable]
    public partial class TeamMemberInfo : AbstractInfo<TeamMemberInfo, ITeamMemberInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "teammembers.teammember";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TeamMemberInfoProvider), OBJECT_TYPE, "TeamMembers.TeamMember", "TeamMemberID", "TeamMemberLastModified", "TeamMemberGuid", null, "Name", null, null, null, null)
        {
            ModuleName = "TeamMembers",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Team member ID.
        /// </summary>
        [DatabaseField]
        public virtual int TeamMemberID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("TeamMemberID"), 0);
            }
            set
            {
                SetValue("TeamMemberID", value);
            }
        }


        /// <summary>
        /// Name.
        /// </summary>
        [DatabaseField]
        public virtual string Name
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Name"), String.Empty);
            }
            set
            {
                SetValue("Name", value);
            }
        }


        /// <summary>
        /// Title.
        /// </summary>
        [DatabaseField]
        public virtual string Title
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Title"), String.Empty);
            }
            set
            {
                SetValue("Title", value, String.Empty);
            }
        }


        /// <summary>
        /// Bio.
        /// </summary>
        [DatabaseField]
        public virtual string Bio
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Bio"), String.Empty);
            }
            set
            {
                SetValue("Bio", value, String.Empty);
            }
        }


        /// <summary>
        /// External link.
        /// </summary>
        [DatabaseField]
        public virtual string ExternalLink
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ExternalLink"), String.Empty);
            }
            set
            {
                SetValue("ExternalLink", value, String.Empty);
            }
        }


        /// <summary>
        /// Image.
        /// </summary>
        [DatabaseField]
        public virtual string Image
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Image"), String.Empty);
            }
            set
            {
                SetValue("Image", value, String.Empty);
            }
        }


        /// <summary>
        /// Team member guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid TeamMemberGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("TeamMemberGuid"), Guid.Empty);
            }
            set
            {
                SetValue("TeamMemberGuid", value);
            }
        }


        /// <summary>
        /// Team member last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime TeamMemberLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("TeamMemberLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("TeamMemberLastModified", value);
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
        protected TeamMemberInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="TeamMemberInfo"/> class.
        /// </summary>
        public TeamMemberInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="TeamMemberInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TeamMemberInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}