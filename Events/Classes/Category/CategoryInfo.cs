using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Events;

[assembly: RegisterObjectType(typeof(CategoryInfo), CategoryInfo.OBJECT_TYPE)]

namespace Events
{
    /// <summary>
    /// Data container class for <see cref="CategoryInfo"/>.
    /// </summary>
    [Serializable]
    public partial class CategoryInfo : AbstractInfo<CategoryInfo, ICategoryInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "events.category";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(CategoryInfoProvider), OBJECT_TYPE, "Events.Category", "CategoryID", "CategoryLastModified", "CategoryGuid", "CategoryCodeName", "CategoryName", null, null, null, null)
        {
            ModuleName = "Events",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Category ID.
        /// </summary>
        [DatabaseField]
        public virtual int CategoryID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("CategoryID"), 0);
            }
            set
            {
                SetValue("CategoryID", value);
            }
        }


        /// <summary>
        /// Category name.
        /// </summary>
        [DatabaseField]
        public virtual string CategoryName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CategoryName"), String.Empty);
            }
            set
            {
                SetValue("CategoryName", value);
            }
        }


        /// <summary>
        /// Category code name.
        /// </summary>
        [DatabaseField]
        public virtual string CategoryCodeName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CategoryCodeName"), String.Empty);
            }
            set
            {
                SetValue("CategoryCodeName", value);
            }
        }


        /// <summary>
        /// Category guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid CategoryGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("CategoryGuid"), Guid.Empty);
            }
            set
            {
                SetValue("CategoryGuid", value);
            }
        }


        /// <summary>
        /// Category last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime CategoryLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("CategoryLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("CategoryLastModified", value);
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
        protected CategoryInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="CategoryInfo"/> class.
        /// </summary>
        public CategoryInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="CategoryInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public CategoryInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}