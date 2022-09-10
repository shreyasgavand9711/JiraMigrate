using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ViewModel
{
    public class TaskTableDataModel
    {
        public List<TaskTableViewModel> Todo { get; set; } = new List<TaskTableViewModel>();
        public List<TaskTableViewModel> Active { get; set; } = new List<TaskTableViewModel>();
        public List<TaskTableViewModel> Completed { get; set; } = new List<TaskTableViewModel>();
    }
}