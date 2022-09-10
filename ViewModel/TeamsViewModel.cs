
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class TeamsViewModel
    {

        public int Id { get; set; }
        public int TeamID { get; set; }

        [Required(ErrorMessage = "Please enter team name")]
        [StringLength(10, ErrorMessage = "action name not be more than 10 charecter")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Please enter team leader name")]
        public Nullable<int> TeamLeder{  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LeaderName { get; set; }
        public int MemberCount { get; set; }

        [Required(ErrorMessage = "Please enter team member name")]
        public List<int> TeamMember1 { get; set; } = new List<int>();
        public List<MemberType> TeamMember { get; set; } = new List<MemberType>();
        public List<CommonDropdownType> TeamLeders { get; set; } = new List<CommonDropdownType>();

        /*public string[] TeamLeders = { "Admin", "Production", "Purchase", "Sale" }*/
    }
}
