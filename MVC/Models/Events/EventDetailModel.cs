using CMS.DocumentEngine;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Events
{
    public class EventDetailModel
    {
        public EventSessionInfo Event { get; set; }
        public TreeNode Page { get; set; }
    }
}
