using CMS.DataEngine;

namespace Events
{
    /// <summary>
    /// Class providing <see cref="EventSessionInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IEventSessionInfoProvider))]
    public partial class EventSessionInfoProvider : AbstractInfoProvider<EventSessionInfo, EventSessionInfoProvider>, IEventSessionInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSessionInfoProvider"/> class.
        /// </summary>
        public EventSessionInfoProvider()
            : base(EventSessionInfo.TYPEINFO)
        {
        }
    }
}