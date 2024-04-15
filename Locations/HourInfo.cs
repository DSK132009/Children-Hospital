using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Locations;

[assembly: RegisterObjectType(typeof(HourInfo), HourInfo.OBJECT_TYPE)]

namespace Locations
{
    /// <summary>
    /// Data container class for <see cref="HourInfo"/>.
    /// </summary>
    [Serializable]
    public partial class HourInfo : AbstractInfo<HourInfo, IHourInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "locations.hour";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(HourInfoProvider), OBJECT_TYPE, "Locations.Hour", "HourID", "HourLastModified", "HourGuid", null, null, null, null, "HourParentId", LocationsHourSetInfo.OBJECT_TYPE)
        {
            ModuleName = "Locations",
            TouchCacheDependencies = true,
            OrderColumn = "HourOrder"
        };


        /// <summary>
        /// Hour ID.
        /// </summary>
        [DatabaseField]
        public virtual int HourID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("HourID"), 0);
            }
            set
            {
                SetValue("HourID", value);
            }
        }


        /// <summary>
        /// Hour parent id.
        /// </summary>
        [DatabaseField]
        public virtual int HourParentId
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("HourParentId"), 0);
            }
            set
            {
                SetValue("HourParentId", value, 0);
            }
        }


        /// <summary>
        /// Hour order.
        /// </summary>
        [DatabaseField]
        public virtual int HourOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("HourOrder"), 0);
            }
            set
            {
                SetValue("HourOrder", value, 0);
            }
        }


        /// <summary>
        /// Start day.
        /// </summary>
        [DatabaseField]
        public virtual string StartDay
        {
            get
            {
                return ValidationHelper.GetString(GetValue("StartDay"), String.Empty);
            }
            set
            {
                SetValue("StartDay", value);
            }
        }


        /// <summary>
        /// End day.
        /// </summary>
        [DatabaseField]
        public virtual string EndDay
        {
            get
            {
                return ValidationHelper.GetString(GetValue("EndDay"), String.Empty);
            }
            set
            {
                SetValue("EndDay", value, String.Empty);
            }
        }


        /// <summary>
        /// Open time.
        /// </summary>
        [DatabaseField]
        public virtual DateTime OpenTime
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("OpenTime"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("OpenTime", value);
            }
        }


        /// <summary>
        /// Close time.
        /// </summary>
        [DatabaseField]
        public virtual DateTime CloseTime
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("CloseTime"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("CloseTime", value);
            }
        }


        /// <summary>
        /// Hour guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid HourGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("HourGuid"), Guid.Empty);
            }
            set
            {
                SetValue("HourGuid", value);
            }
        }


        /// <summary>
        /// Hour last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime HourLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("HourLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("HourLastModified", value);
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
        protected HourInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="HourInfo"/> class.
        /// </summary>
        public HourInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="HourInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public HourInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}