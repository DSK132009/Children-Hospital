using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMembers;

namespace MVC.Models.TeamMember
{
    public class TeamMemberDetailModel
    {
        public TreeNode Page { get; set; }
        public TeamMemberInfo TeamMember { get; set; }
    }
}
