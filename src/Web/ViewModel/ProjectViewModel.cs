using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModel
{
    public class ProjectViewModel
    {
        public IList<ProjectModel> Projects { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
