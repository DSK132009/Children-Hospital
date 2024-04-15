using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Declares members for <see cref="InactiveAttendeeInfo"/> management.
    /// </summary>
    public partial interface IInactiveAttendeeInfoProvider : IInfoProvider<InactiveAttendeeInfo>, IInfoByIdProvider<InactiveAttendeeInfo>
    {
    }
}