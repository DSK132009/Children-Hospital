using CMS.DataEngine;

namespace URLRedirection
{
    /// <summary>
    /// Class providing <see cref="RedirectionInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IRedirectionInfoProvider))]
    public partial class RedirectionInfoProvider : AbstractInfoProvider<RedirectionInfo, RedirectionInfoProvider>, IRedirectionInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectionInfoProvider"/> class.
        /// </summary>
        public RedirectionInfoProvider()
            : base(RedirectionInfo.TYPEINFO)
        {
        }
    }
}