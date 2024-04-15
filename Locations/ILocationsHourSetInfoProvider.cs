using CMS.DataEngine;

namespace Locations
{
    /// <summary>
    /// Declares members for <see cref="LocationsHourSetInfo"/> management.
    /// </summary>
    public partial interface ILocationsHourSetInfoProvider : IInfoProvider<LocationsHourSetInfo>, IInfoByIdProvider<LocationsHourSetInfo>, IInfoByNameProvider<LocationsHourSetInfo>
    {
    }
}