using ApplicationCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore
{
    public class TaskBLO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Timing { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int StatusId { get; set; }
        public int EmployeeId { get; set; }

        public EmployeeBLO Employee { get; set; }
        public ProjectBLO Project { get; set; }
        public StatusBLO Status { get; set; }
    }
}
