using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int PageTotal { get; private set; }
        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageTotal = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
