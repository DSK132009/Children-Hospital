using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Events;

[assembly: RegisterObjectType(typeof(ActiveAttendeeInfo), ActiveAttendeeInfo.OBJECT_TYPE)]

namespace Events
{
    /// <summary>
    /// Data container class for <see cref="ActiveAttendeeInfo"/>.
    /// </summary>
    [Serializable]
    public partial class ActiveAttendeeInfo : AbstractInfo<ActiveAttendeeInfo, IActiveAttendeeInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "events.activeattendee";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ActiveAttendeeInfoProvider), OBJECT_TYPE, "Events.ActiveAttendee", "ActiveAttendeeID", "ActiveAttendeeLastModified", "ActiveAttendeeGuid", null, "FirstName", null, null, "ParentEventID", EventSessionInfo.OBJECT_TYPE)
        {
            ModuleName = "Events",
            TouchCacheDependencies = true
        };


        /// <summary>
        /// Active attendee ID.
        /// </summary>
        [DatabaseField]
        public virtual int ActiveAttendeeID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ActiveAttendeeID"), 0);
            }
            set
            {
                SetValue("ActiveAttendeeID", value);
            }
        }


        /// <summary>
        /// Parent event ID.
        /// </summary>
        [DatabaseField]
        public virtual int ParentEventID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ParentEventID"), 0);
            }
            set
            {
                SetValue("ParentEventID", value);
            }
        }


        /// <summary>
        /// First name.
        /// </summary>
        [DatabaseField]
        public virtual string FirstName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FirstName"), String.Empty);
            }
            set
            {
                SetValue("FirstName", value);
            }
        }


        /// <summary>
        /// Middle initial.
        /// </summary>
        [DatabaseField]
        public virtual string MiddleInitial
        {
            get
            {
                return ValidationHelper.GetString(GetValue("MiddleInitial"), String.Empty);
            }
            set
            {
                SetValue("MiddleInitial", value, String.Empty);
            }
        }


        /// <summary>
        /// Last name.
        /// </summary>
        [DatabaseField]
        public virtual string LastName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LastName"), String.Empty);
            }
            set
            {
                SetValue("LastName", value);
            }
        }


        /// <summary>
        /// Phone.
        /// </summary>
        [DatabaseField]
        public virtual string Phone
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Phone"), String.Empty);
            }
            set
            {
                SetValue("Phone", value);
            }
        }


        /// <summary>
        /// Email.
        /// </summary>
        [DatabaseField]
        public virtual string Email
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Email"), String.Empty);
            }
            set
            {
                SetValue("Email", value);
            }
        }


        /// <summary>
        /// PRNR.
        /// </summary>
        [DatabaseField]
        public virtual string PRNR
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PRNR"), String.Empty);
            }
            set
            {
                SetValue("PRNR", value, String.Empty);
            }
        }


        /// <summary>
        /// Comment.
        /// </summary>
        [DatabaseField]
        public virtual string Comment
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Comment"), String.Empty);
            }
            set
            {
                SetValue("Comment", value, String.Empty);
            }
        }


        /// <summary>
        /// Date added.
        /// </summary>
        [DatabaseField]
        public virtual DateTime DateAdded
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("DateAdded"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("DateAdded", value);
            }
        }


        /// <summary>
        /// Active attendee guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid ActiveAttendeeGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("ActiveAttendeeGuid"), Guid.Empty);
            }
            set
            {
                SetValue("ActiveAttendeeGuid", value);
            }
        }


        /// <summary>
        /// Active attendee last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime ActiveAttendeeLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("ActiveAttendeeLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ActiveAttendeeLastModified", value);
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
        protected ActiveAttendeeInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="ActiveAttendeeInfo"/> class.
        /// </summary>
        public ActiveAttendeeInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="ActiveAttendeeInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ActiveAttendeeInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}