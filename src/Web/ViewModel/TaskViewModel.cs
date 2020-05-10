using Web.Models;
using System.Collections.Generic;


namespace Web.ViewModel
{
    public class TaskViewModel
    {
        public IList<TaskModel> Tasks{ get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
