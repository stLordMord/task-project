using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure
{
    public class TaskDTO
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Timing { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int StatusId { get; set; }
        public int EmployeeId { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
        public virtual ProjectDTO Project { get; set; }
        public virtual StatusDTO Status {get; set;}
    }
}
