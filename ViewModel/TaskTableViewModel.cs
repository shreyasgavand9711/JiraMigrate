using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ViewModel
{
    public class TaskTableViewModel
    {
        public int taskId { get; set; }

        [Required(ErrorMessage = "please enter task name")]
        public string taskName { get; set; }
        public int? taskStatus { get; set; }

        [Required(ErrorMessage = "pleas enter description")]
        public string description { get; set; }

        public int? assigneeId { get; set; }
        public int? reporterId { get; set; }
        public string userName { get; set; }

        public int sprintName { get; set; }
        public int IssueId { get; set; }

        public int issueType { get; set; }

        public string IssueName { get; set; }
        //public List<FlagType> Flags { get; set; } = new List<FlagType>();

        public List<int> Flags { get; set; } = new List<int>();
        public List<MemberType> Flags2 { get; set; } = new List<MemberType>();
        public List<CommonDropdownType> taskStatusList { get; set; } = new List<CommonDropdownType>();
        public List<CommonDropdownType> userList { get; set; } = new List<CommonDropdownType>();
        public List<CommonDropdownType> FlagList { get; set; } = new List<CommonDropdownType>();

        public List<int> LableName { get; set; } = new List<int>();

        public List<CommonDropdownType> labelList { get; set; } = new List<CommonDropdownType>();

        public List<CommonDropdownType> sprintList { get; set; } = new List<CommonDropdownType>();

        public List<CommonDropdownType> IssueTypeList { get; set; } = new List<CommonDropdownType>();
        public List<CommonDropdownType> IssueTaskList { get; set; } = new List<CommonDropdownType>();

        public List<CommonDropdownType> ComplexityList { get; set; } = new List<CommonDropdownType>();
        public List<int> issuetaskName { get; set; } = new List<int>();


    }
}
