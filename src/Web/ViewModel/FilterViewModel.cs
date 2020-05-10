using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModel
{
    public class FilterViewModel
    {
        public FilterViewModel(string searchText)
        {
            SearchText = searchText;
        }
        public string SearchText { get; private set; } 
    }
}
