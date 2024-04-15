using CMS.DataEngine;

namespace Alerts
{
    /// <summary>
    /// Class providing <see cref="AlertInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IAlertInfoProvider))]
    public partial class AlertInfoProvider : AbstractInfoProvider<AlertInfo, AlertInfoProvider>, IAlertInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertInfoProvider"/> class.
        /// </summary>
        public AlertInfoProvider()
            : base(AlertInfo.TYPEINFO)
        {
        }
    }
}