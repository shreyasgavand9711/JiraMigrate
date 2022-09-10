using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserActions
    {
        [Required]
        public int ActionId { get; set; }


        [Required(ErrorMessage = "please enter controller name")]
        [StringLength(25, ErrorMessage = "controller name  not be more than 25 charecter ")]
        public string ControllerName { get; set; }

        [Required(ErrorMessage = "please enter action name")]
        [StringLength(25, ErrorMessage = "action name not be more than 25 charecter ")]
        public string ActionName { get; set; }

        
        public Boolean ShowInSideBar { get; set; }

        [Required(ErrorMessage = "please enter display name")]
        [StringLength(50, ErrorMessage = "Display Name not be more than 50 charecter ")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "please select role")]
        public List<int> AccessRolls { get; set; } = new List<int>();

    }



}
