using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Class providing <see cref="ActiveAttendeeInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IActiveAttendeeInfoProvider))]
    public partial class ActiveAttendeeInfoProvider : AbstractInfoProvider<ActiveAttendeeInfo, ActiveAttendeeInfoProvider>, IActiveAttendeeInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveAttendeeInfoProvider"/> class.
        /// </summary>
        public ActiveAttendeeInfoProvider()
            : base(ActiveAttendeeInfo.TYPEINFO)
        {
        }
    }
}