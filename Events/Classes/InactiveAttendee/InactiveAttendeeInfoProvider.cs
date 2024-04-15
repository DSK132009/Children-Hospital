using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Class providing <see cref="InactiveAttendeeInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IInactiveAttendeeInfoProvider))]
    public partial class InactiveAttendeeInfoProvider : AbstractInfoProvider<InactiveAttendeeInfo, InactiveAttendeeInfoProvider>, IInactiveAttendeeInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InactiveAttendeeInfoProvider"/> class.
        /// </summary>
        public InactiveAttendeeInfoProvider()
            : base(InactiveAttendeeInfo.TYPEINFO)
        {
        }
    }
}