using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.entits
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool AllowUserCreation { get; set; }

        public bool AllowTeamCreation { get; set; }




    }
}
