using CMS.DataEngine;

namespace Locations
{
    /// <summary>
    /// Class providing <see cref="LocationsHourSetInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ILocationsHourSetInfoProvider))]
    public partial class LocationsHourSetInfoProvider : AbstractInfoProvider<LocationsHourSetInfo, LocationsHourSetInfoProvider>, ILocationsHourSetInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsHourSetInfoProvider"/> class.
        /// </summary>
        public LocationsHourSetInfoProvider()
            : base(LocationsHourSetInfo.TYPEINFO)
        {
        }
    }
}