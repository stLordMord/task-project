using ApplicationCore;
using Common;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converter
{
    public class ProjectConverter : IConverter<ProjectDTO, ProjectBLO>
    {
        private readonly ILogger<ProjectConverter> logger;
        public ProjectConverter(ILogger<ProjectConverter> logger)
        {
            this.logger = logger;
        }
        public IList<ProjectBLO> Convert(IList<ProjectDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<ProjectBLO> projects = new List<ProjectBLO>();
            foreach (ProjectDTO projectDTO in listDTO)
            {
                ProjectBLO objBLO = new ProjectBLO()
                {
                    Id = projectDTO.Id,
                    Name = projectDTO.Name,
                    ShortName = projectDTO.ShortName,
                    Description = projectDTO.Description
                };
                projects.Add(objBLO);
            }
            return projects;
        }

        public ProjectBLO Convert(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(projectDTO));
            }
            ProjectBLO projectBLO = new ProjectBLO()
            {
                Id = projectDTO.Id,
                Name = projectDTO.Name,
                ShortName = projectDTO.ShortName,
                Description = projectDTO.Description,
                Tasks = Convert(projectDTO.Tasks)
            };
            return projectBLO;
        }

        private IList<TaskBLO> Convert(IList<TaskDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<TaskBLO> tasks = new List<TaskBLO>();
            foreach (TaskDTO taskDTO in listDTO)
            {
                TaskBLO taskBLO = new TaskBLO()
                {
                    Id = taskDTO.Id,
                    ProjectId = taskDTO.ProjectId,
                    Name = taskDTO.Name,
                    Timing = taskDTO.Timing,
                    DateStart = taskDTO.DateStart,
                    DateEnd = taskDTO.DateEnd,
                    StatusId = taskDTO.StatusId,
                    EmployeeId = taskDTO.EmployeeId,
                    Employee = new EmployeeBLO()
                    {
                        Id = taskDTO.Employee.Id,
                        Name = taskDTO.Employee.Name,
                        Surname = taskDTO.Employee.Surname,
                        Patronymic = taskDTO.Employee.Patronymic,
                        PositionId = taskDTO.Employee.PositionId,
                        Position = new PositionBLO()
                        {
                            Id = taskDTO.Employee.Position.Id,
                            Name = taskDTO.Employee.Position.Name
                        }
                    },
                    Status = new StatusBLO()
                    {
                        Id = taskDTO.Status.Id,
                        Name = taskDTO.Status.Name
                    }
                };
                tasks.Add(taskBLO);
            }
            return tasks;
        }

        public ProjectDTO Convert(ProjectBLO projectBLO)
        {
            if (projectBLO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(projectBLO));
            }
            ProjectDTO projectDTO = new ProjectDTO()
            {
                Id = projectBLO.Id,
                Name = projectBLO.Name,
                ShortName = projectBLO.ShortName,
                Description = projectBLO.Description
            };
            return projectDTO;
        }
    }
}
