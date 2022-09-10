using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.entits
{
    public class Actions
    {
        public int ActionId { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public Boolean ShowInSideBar { get; set; }
        public string DisplayName { get; set; }

        public DataTable ActionAccess { get; set; }
    }

}
