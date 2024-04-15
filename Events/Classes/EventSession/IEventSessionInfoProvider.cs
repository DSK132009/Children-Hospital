using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Declares members for <see cref="EventSessionInfo"/> management.
    /// </summary>
    public partial interface IEventSessionInfoProvider : IInfoProvider<EventSessionInfo>, IInfoByIdProvider<EventSessionInfo>, IInfoByNameProvider<EventSessionInfo>
    {
    }
}