using ApplicationCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore
{
    public class EmployeeBLO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int PositionId { get; set; }
        public PositionBLO Position {get; set;}
    }
}
