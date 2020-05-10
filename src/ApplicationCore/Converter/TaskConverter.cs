using ApplicationCore;
using Common;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converter
{
    public class TaskConverter : IConverter<TaskDTO, TaskBLO>
    {
        private readonly ILogger<TaskConverter> logger;
        public TaskConverter(ILogger<TaskConverter> logger)
        {
            this.logger = logger;
        }
        public IList<TaskBLO> Convert(IList<TaskDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<TaskBLO> tasks = new List<TaskBLO>();
            foreach (TaskDTO taskDTO in listDTO)
            {
                TaskBLO objBLO = Convert(taskDTO);
                tasks.Add(objBLO);
            }
            return tasks;
        }

        public TaskBLO Convert(TaskDTO taskDTO)
        {
            if (taskDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(taskDTO));
            }
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
                Project = new ProjectBLO()
                {
                    Id = taskDTO.Project.Id,
                    Name = taskDTO.Project.Name,
                    ShortName = taskDTO.Project.ShortName,
                    Description = taskDTO.Project.Description
                },
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
                        Name = taskDTO.Employee.Position.Name,
                    }
                },
                Status = new StatusBLO()
                {
                    Id = taskDTO.Status.Id,
                    Name = taskDTO.Status.Name
                }
            };
            return taskBLO;
        }

        public TaskDTO Convert(TaskBLO taskBLO)
        {
            if (taskBLO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(taskBLO));
            }
            TaskDTO taskDTO = new TaskDTO()
            {
                Id = taskBLO.Id,
                ProjectId = taskBLO.ProjectId,
                Name = taskBLO.Name,
                Timing = taskBLO.Timing,
                DateStart = taskBLO.DateStart,
                DateEnd = taskBLO.DateEnd,
                StatusId = taskBLO.StatusId,
                EmployeeId = taskBLO.EmployeeId
            };
            return taskDTO;
        }
    }
}
