using CMS.DataEngine;

namespace Locations
{
    /// <summary>
    /// Class providing <see cref="HourInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IHourInfoProvider))]
    public partial class HourInfoProvider : AbstractInfoProvider<HourInfo, HourInfoProvider>, IHourInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HourInfoProvider"/> class.
        /// </summary>
        public HourInfoProvider()
            : base(HourInfo.TYPEINFO)
        {
        }
    }
}