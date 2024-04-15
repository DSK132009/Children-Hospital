using CMS.DataEngine;

namespace Locations
{
    /// <summary>
    /// Declares members for <see cref="LocationInfo"/> management.
    /// </summary>
    public partial interface ILocationInfoProvider : IInfoProvider<LocationInfo>, IInfoByIdProvider<LocationInfo>, IInfoByNameProvider<LocationInfo>
    {
    }
}