using CMS.DataEngine;

namespace TeamMembers
{
    /// <summary>
    /// Class providing <see cref="TeamMemberInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ITeamMemberInfoProvider))]
    public partial class TeamMemberInfoProvider : AbstractInfoProvider<TeamMemberInfo, TeamMemberInfoProvider>, ITeamMemberInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberInfoProvider"/> class.
        /// </summary>
        public TeamMemberInfoProvider()
            : base(TeamMemberInfo.TYPEINFO)
        {
        }
    }
}