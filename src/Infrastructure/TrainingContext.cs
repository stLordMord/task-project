using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class TrainingContext : DbContext
    {
        public TrainingContext(DbContextOptions<TrainingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeDTO> Employees { get; set; }
        public virtual DbSet<ProjectDTO> Projects { get; set; }
        public virtual DbSet<TaskDTO> Tasks { get; set; }
        public virtual DbSet<StatusDTO> Statuses { get; set; }
        public virtual DbSet<PositionDTO> Positions { get; set; }

    }
}
