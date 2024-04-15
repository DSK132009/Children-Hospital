using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Events;

[assembly: RegisterObjectType(typeof(EventSessionInfo), EventSessionInfo.OBJECT_TYPE)]

namespace Events
{
    /// <summary>
    /// Data container class for <see cref="EventSessionInfo"/>.
    /// </summary>
    [Serializable]
    public partial class EventSessionInfo : AbstractInfo<EventSessionInfo, IEventSessionInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "events.eventsession";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(EventSessionInfoProvider), OBJECT_TYPE, "Events.EventSession", "EventSessionID", "EventSessionLastModified", "EventSessionGuid", "EventCodeName", "EventName", null, null, null, null)
        {
            ModuleName = "Events",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Event session ID.
        /// </summary>
        [DatabaseField]
        public virtual int EventSessionID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EventSessionID"), 0);
            }
            set
            {
                SetValue("EventSessionID", value);
            }
        }


        /// <summary>
        /// Event name.
        /// </summary>
        [DatabaseField]
        public virtual string EventName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EventName"), String.Empty);
            }
            set
            {
                SetValue("EventName", value);
            }
        }


        /// <summary>
        /// Event code name.
        /// </summary>
        [DatabaseField]
        public virtual string EventCodeName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EventCodeName"), String.Empty);
            }
            set
            {
                SetValue("EventCodeName", value);
            }
        }


        /// <summary>
        /// Short summary.
        /// </summary>
        [DatabaseField]
        public virtual string ShortSummary
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ShortSummary"), String.Empty);
            }
            set
            {
                SetValue("ShortSummary", value);
            }
        }


        /// <summary>
        /// Description.
        /// </summary>
        [DatabaseField]
        public virtual string Description
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Description"), String.Empty);
            }
            set
            {
                SetValue("Description", value);
            }
        }


        /// <summary>
        /// Hero image.
        /// </summary>
        [DatabaseField]
        public virtual string HeroImage
        {
            get
            {
                return ValidationHelper.GetString(GetValue("HeroImage"), String.Empty);
            }
            set
            {
                SetValue("HeroImage", value, String.Empty);
            }
        }


        /// <summary>
        /// Location guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid LocationGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("LocationGuid"), Guid.Empty);
            }
            set
            {
                SetValue("LocationGuid", value);
            }
        }


        /// <summary>
        /// Event category.
        /// </summary>
        [DatabaseField]
        public virtual string EventCategory
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EventCategory"), String.Empty);
            }
            set
            {
                SetValue("EventCategory", value);
            }
        }


        /// <summary>
        /// Event type.
        /// </summary>
        [DatabaseField]
        public virtual string EventType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EventType"), String.Empty);
            }
            set
            {
                SetValue("EventType", value);
            }
        }


        /// <summary>
        /// Cost.
        /// </summary>
        [DatabaseField]
        public virtual string Cost
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Cost"), String.Empty);
            }
            set
            {
                SetValue("Cost", value);
            }
        }


        /// <summary>
        /// Event size.
        /// </summary>
        [DatabaseField]
        public virtual int EventSize
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("EventSize"), 0);
            }
            set
            {
                SetValue("EventSize", value);
            }
        }


        /// <summary>
        /// Contact email.
        /// </summary>
        [DatabaseField]
        public virtual string ContactEmail
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ContactEmail"), String.Empty);
            }
            set
            {
                SetValue("ContactEmail", value);
            }
        }


        /// <summary>
        /// Event start.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EventStart
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("EventStart"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EventStart", value);
            }
        }


        /// <summary>
        /// Event end.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EventEnd
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("EventEnd"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EventEnd", value);
            }
        }


        /// <summary>
        /// Event session guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid EventSessionGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("EventSessionGuid"), Guid.Empty);
            }
            set
            {
                SetValue("EventSessionGuid", value);
            }
        }


        /// <summary>
        /// Event session last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime EventSessionLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("EventSessionLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("EventSessionLastModified", value);
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
        protected EventSessionInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="EventSessionInfo"/> class.
        /// </summary>
        public EventSessionInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="EventSessionInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public EventSessionInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}