using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserSignupViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "User name lenght is not more than 20 character")]
       // [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special character should not be entered")]
        public string Username { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Password lenght is more than 6 and lass than 10  character", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First name lenght is not more than 20 character")]
        //[RegularExpression(@"^(?=[^\s]*?[0-9])(?=[^\s]*?[a-zA-Z])[a-zA-Z0-9]*$", ErrorMessage = "Enter only character")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter only character")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last name lenght is not more than 20 character")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter only character")]
        public string LastName { get; set; }
        public string Name { get; set; }

        public List<CommonDropdownType> RoleIdList { get; set; } = new List<CommonDropdownType>();
    }

    
}
