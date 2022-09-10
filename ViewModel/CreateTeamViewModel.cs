using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class CreateTeamViewModel
    {


        [Required(ErrorMessage = "please enter team name")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "please enter team leader name")]
        public Nullable<int> TeamLeder
        {
            get; set;


        }
        [Required(ErrorMessage = "please enter Team member name")]
        public List<MemberType> TeamMember { get; set; } = new List<MemberType>();
    }
}
