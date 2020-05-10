using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure
{
    public class PositionDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
