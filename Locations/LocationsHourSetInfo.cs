using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Locations;

[assembly: RegisterObjectType(typeof(LocationsHourSetInfo), LocationsHourSetInfo.OBJECT_TYPE)]

namespace Locations
{
    /// <summary>
    /// Data container class for <see cref="LocationsHourSetInfo"/>.
    /// </summary>
    [Serializable]
    public partial class LocationsHourSetInfo : AbstractInfo<LocationsHourSetInfo, ILocationsHourSetInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "locations.locationshourset";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(LocationsHourSetInfoProvider), OBJECT_TYPE, "Locations.LocationsHourSet", "LocationsHourSetID", "LocationsHourSetLastModified", "LocationsHourSetGuid", "SetName", "SetDisplayName", null, null, "SetParentId", LocationInfo.OBJECT_TYPE)
        {
            ModuleName = "Locations",
            TouchCacheDependencies = true,
            OrderColumn = "SetOrder"
        };


        /// <summary>
        /// Locations hour set ID.
        /// </summary>
        [DatabaseField]
        public virtual int LocationsHourSetID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("LocationsHourSetID"), 0);
            }
            set
            {
                SetValue("LocationsHourSetID", value);
            }
        }


        /// <summary>
        /// Set parent id.
        /// </summary>
        [DatabaseField]
        public virtual int SetParentId
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SetParentId"), 0);
            }
            set
            {
                SetValue("SetParentId", value, 0);
            }
        }


        /// <summary>
        /// Set order.
        /// </summary>
        [DatabaseField]
        public virtual int SetOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SetOrder"), 0);
            }
            set
            {
                SetValue("SetOrder", value, 0);
            }
        }


        /// <summary>
        /// Set name.
        /// </summary>
        [DatabaseField]
        public virtual string SetName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SetName"), String.Empty);
            }
            set
            {
                SetValue("SetName", value);
            }
        }


        /// <summary>
        /// Set display name.
        /// </summary>
        [DatabaseField]
        public virtual string SetDisplayName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SetDisplayName"), String.Empty);
            }
            set
            {
                SetValue("SetDisplayName", value, String.Empty);
            }
        }


        /// <summary>
        /// Set enabled.
        /// </summary>
        [DatabaseField]
        public virtual bool SetEnabled
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("SetEnabled"), true);
            }
            set
            {
                SetValue("SetEnabled", value);
            }
        }


        /// <summary>
        /// Locations hour set guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid LocationsHourSetGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("LocationsHourSetGuid"), Guid.Empty);
            }
            set
            {
                SetValue("LocationsHourSetGuid", value);
            }
        }


        /// <summary>
        /// Locations hour set last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime LocationsHourSetLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("LocationsHourSetLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("LocationsHourSetLastModified", value);
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
        protected LocationsHourSetInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="LocationsHourSetInfo"/> class.
        /// </summary>
        public LocationsHourSetInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="LocationsHourSetInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public LocationsHourSetInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}