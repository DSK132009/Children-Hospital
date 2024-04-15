using CMS.DataEngine;

namespace TeamMembers
{
    /// <summary>
    /// Declares members for <see cref="TeamMemberInfo"/> management.
    /// </summary>
    public partial interface ITeamMemberInfoProvider : IInfoProvider<TeamMemberInfo>, IInfoByIdProvider<TeamMemberInfo>, IInfoByNameProvider<TeamMemberInfo>
    {
    }
}