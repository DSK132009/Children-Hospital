using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Declares members for <see cref="ActiveAttendeeInfo"/> management.
    /// </summary>
    public partial interface IActiveAttendeeInfoProvider : IInfoProvider<ActiveAttendeeInfo>, IInfoByIdProvider<ActiveAttendeeInfo>
    {
    }
}