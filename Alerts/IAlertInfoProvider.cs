using CMS.DataEngine;

namespace Alerts
{
    /// <summary>
    /// Declares members for <see cref="AlertInfo"/> management.
    /// </summary>
    public partial interface IAlertInfoProvider : IInfoProvider<AlertInfo>, IInfoByIdProvider<AlertInfo>
    {
    }
}