using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Locations;

[assembly: RegisterObjectType(typeof(LocationInfo), LocationInfo.OBJECT_TYPE)]

namespace Locations
{
    /// <summary>
    /// Data container class for <see cref="LocationInfo"/>.
    /// </summary>
    [Serializable]
    public partial class LocationInfo : AbstractInfo<LocationInfo, ILocationInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "locations.location";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(LocationInfoProvider), OBJECT_TYPE, "Locations.Location", "LocationID", "LocationLastModified", "LocationGuid", null, "LocationName", null, null, null, null)
        {
            ModuleName = "Locations",
            TouchCacheDependencies = true,
            OrderColumn = "LocationOrder"
        };


        /// <summary>
        /// Location ID.
        /// </summary>
        [DatabaseField]
        public virtual int LocationID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("LocationID"), 0);
            }
            set
            {
                SetValue("LocationID", value);
            }
        }


        /// <summary>
        /// Location name.
        /// </summary>
        [DatabaseField]
        public virtual string LocationName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationName"), String.Empty);
            }
            set
            {
                SetValue("LocationName", value);
            }
        }


        /// <summary>
        /// Street address.
        /// </summary>
        [DatabaseField]
        public virtual string StreetAddress
        {
            get
            {
                return ValidationHelper.GetString(GetValue("StreetAddress"), String.Empty);
            }
            set
            {
                SetValue("StreetAddress", value);
            }
        }


        /// <summary>
        /// Street address 2.
        /// </summary>
        [DatabaseField]
        public virtual string StreetAddress2
        {
            get
            {
                return ValidationHelper.GetString(GetValue("StreetAddress2"), String.Empty);
            }
            set
            {
                SetValue("StreetAddress2", value, String.Empty);
            }
        }


        /// <summary>
        /// City.
        /// </summary>
        [DatabaseField]
        public virtual string City
        {
            get
            {
                return ValidationHelper.GetString(GetValue("City"), String.Empty);
            }
            set
            {
                SetValue("City", value);
            }
        }


        /// <summary>
        /// State.
        /// </summary>
        [DatabaseField]
        public virtual string State
        {
            get
            {
                return ValidationHelper.GetString(GetValue("State"), String.Empty);
            }
            set
            {
                SetValue("State", value);
            }
        }


        /// <summary>
        /// Zip.
        /// </summary>
        [DatabaseField]
        public virtual string Zip
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Zip"), String.Empty);
            }
            set
            {
                SetValue("Zip", value);
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
        /// Location type.
        /// </summary>
        [DatabaseField]
        public virtual string LocationType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationType"), String.Empty);
            }
            set
            {
                SetValue("LocationType", value);
            }
        }


        /// <summary>
        /// Location image.
        /// </summary>
        [DatabaseField]
        public virtual string LocationImage
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationImage"), String.Empty);
            }
            set
            {
                SetValue("LocationImage", value, String.Empty);
            }
        }


        /// <summary>
        /// Location image alt text.
        /// </summary>
        [DatabaseField]
        public virtual string LocationImageAltText
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationImageAltText"), String.Empty);
            }
            set
            {
                SetValue("LocationImageAltText", value, String.Empty);
            }
        }


        /// <summary>
        /// Latitude.
        /// </summary>
        [DatabaseField]
        public virtual decimal Latitude
        {
            get
            {
                return ValidationHelper.GetDecimal(GetValue("Latitude"), 0m);
            }
            set
            {
                SetValue("Latitude", value, 0m);
            }
        }


        /// <summary>
        /// Longitude.
        /// </summary>
        [DatabaseField]
        public virtual decimal Longitude
        {
            get
            {
                return ValidationHelper.GetDecimal(GetValue("Longitude"), 0m);
            }
            set
            {
                SetValue("Longitude", value, 0m);
            }
        }


        /// <summary>
        /// Google maps link.
        /// </summary>
        [DatabaseField]
        public virtual string GoogleMapsLink
        {
            get
            {
                return ValidationHelper.GetString(GetValue("GoogleMapsLink"), String.Empty);
            }
            set
            {
                SetValue("GoogleMapsLink", value, String.Empty);
            }
        }


        /// <summary>
        /// Location description.
        /// </summary>
        [DatabaseField]
        public virtual string LocationDescription
        {
            get
            {
                return ValidationHelper.GetString(GetValue("LocationDescription"), String.Empty);
            }
            set
            {
                SetValue("LocationDescription", value, String.Empty);
            }
        }

        /// <summary>
        /// Location order.
        /// </summary>
        [DatabaseField]
        public virtual int LocationOrder
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("LocationOrder"), 0);
            }
            set
            {
                SetValue("LocationOrder", value, 0);
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
        /// Location last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime LocationLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("LocationLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("LocationLastModified", value);
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
        protected LocationInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="LocationInfo"/> class.
        /// </summary>
        public LocationInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="LocationInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public LocationInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}