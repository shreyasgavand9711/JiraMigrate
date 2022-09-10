using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class FlagViewModel
    {
        public int FlagId { get; set; }

        [Required(ErrorMessage = "Please enter flag name")]
        [StringLength(10, ErrorMessage = "Flag name not be more than 10 charecter")]
        public string FlagName { get; set; }
    }
}
