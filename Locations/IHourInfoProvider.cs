using CMS.DataEngine;

namespace Locations
{
    /// <summary>
    /// Declares members for <see cref="HourInfo"/> management.
    /// </summary>
    public partial interface IHourInfoProvider : IInfoProvider<HourInfo>, IInfoByIdProvider<HourInfo>
    {
    }
}