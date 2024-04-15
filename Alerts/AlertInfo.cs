using System;
using System.Data;
using System.Runtime.Serialization;
using Alerts;
using CMS;
using CMS.DataEngine;
using CMS.Helpers;

[assembly: RegisterObjectType(typeof(AlertInfo), AlertInfo.OBJECT_TYPE)]

namespace Alerts
{
    /// <summary>
    /// Data container class for <see cref="AlertInfo"/>.
    /// </summary>
    [Serializable]
    public partial class AlertInfo : AbstractInfo<AlertInfo, IAlertInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "alerts.alert";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(AlertInfoProvider), OBJECT_TYPE, "Alerts.Alert", "AlertID", "AlertLastModified", "AlertGuid", null, "AlertName", null, null, null, null)
        {
            ModuleName = "Alerts",
            TouchCacheDependencies = true,
            OrderColumn = "AlertOrder"
        };


        /// <summary>
        /// Alert ID.
        /// </summary>
        [DatabaseField]
        public virtual int AlertID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("AlertID"), 0);
            }
            set
            {
                SetValue("AlertID", value);
            }
        }


        /// <summary>
        /// Alert guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid AlertGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("AlertGuid"), Guid.Empty);
            }
            set
            {
                SetValue("AlertGuid", value);
            }
        }


        /// <summary>
        /// Alert last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime AlertLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("AlertLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("AlertLastModified", value);
            }
        }

        /// <summary>
        /// Alert text.
        /// </summary>
        [DatabaseField]
        public virtual string AlertName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("AlertName"), String.Empty);
            }
            set
            {
                SetValue("AlertName", value, String.Empty);
            }
        }

        /// <summary>
        /// Alert text.
        /// </summary>
        [DatabaseField]
        public virtual string AlertText
        {
            get
            {
                return ValidationHelper.GetString(GetValue("AlertText"), String.Empty);
            }
            set
            {
                SetValue("AlertText", value, String.Empty);
            }
        }


        /// <summary>
        /// Alert enabled.
        /// </summary>
        [DatabaseField]
        public virtual bool AlertEnabled
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("AlertEnabled"), false);
            }
            set
            {
                SetValue("AlertEnabled", value);
            }
        }

        /// <summary>
        /// Alert publish from.
        /// </summary>
        [DatabaseField]
        public virtual DateTime AlertPublishFrom
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("AlertPublishFrom"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("AlertPublishFrom", value, DateTimeHelper.ZERO_TIME);
            }
        }


        /// <summary>
        /// Alert publish to.
        /// </summary>
        [DatabaseField]
        public virtual DateTime AlertPublishTo
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("AlertPublishTo"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("AlertPublishTo", value, DateTimeHelper.ZERO_TIME);
            }
        }

        /// <summary>
        /// Alert order.
        /// </summary>
        [DatabaseField]
        public virtual int AlertOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("AlertOrder"), 0);
            }
            set
            {
                SetValue("AlertOrder", value);
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
        protected AlertInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="AlertInfo"/> class.
        /// </summary>
        public AlertInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="AlertInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public AlertInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}