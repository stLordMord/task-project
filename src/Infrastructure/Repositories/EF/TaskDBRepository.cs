using Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class TaskDBRepository : IRepository<TaskDTO>
    {
        private readonly ILogger<IRepository<TaskDTO>> logger;
        private readonly TrainingContext context;
        private DbSet<TaskDTO> taskDTOs;
        private DbSet<ProjectDTO> projectDTOs;
        private DbSet<EmployeeDTO> employeeDTOs;
        private DbSet<StatusDTO> statusDTOs;
        private DbSet<PositionDTO> positionDTOs;

        public TaskDBRepository(TrainingContext context, ILogger<IRepository<TaskDTO>> logger)
        {
            this.logger = logger;
            this.context = context;
            taskDTOs = context.Tasks;
            projectDTOs = context.Projects;
            employeeDTOs = context.Employees;
            statusDTOs = context.Statuses;
            positionDTOs = context.Positions;
        }

        public int GetCount(string searchText)
        {
            try
            {
                logger.LogTrace("Метод GetCount на уровне TaskDBRepository");
                int count = taskDTOs.Count(p => p.Name.Contains(searchText));
                return count;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось получить количество задач через EF");
            }
        }

        public IList<TaskDTO> GetAll(int page, int size, string searchText)
        {
            logger.LogTrace("Метод GetAll на уровне TaskDBRepository");
            var tasks = new List<TaskDTO>();
            try
            {
                tasks = taskDTOs.Where(p => p.Name.Contains(searchText))
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();

                foreach (var t in tasks)
                {
                    t.Project = projectDTOs.FirstOrDefault(p => p.Id == t.ProjectId);
                    t.Employee = employeeDTOs.FirstOrDefault(p => p.Id == t.EmployeeId);
                    t.Employee.Position = positionDTOs.FirstOrDefault(q => q.Id == t.Employee.PositionId);
                    t.Status = statusDTOs.FirstOrDefault(p => p.Id == t.StatusId);
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить список Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось получить список задач в БД через EF");
            }
            return tasks;
        }

        public TaskDTO GetById(int id)
        {
            logger.LogTrace("Метод GetById на уровне TaskDBRepository");
            TaskDTO task = new TaskDTO();
            try
            {
                task = taskDTOs.FirstOrDefault(c => c.Id == id);
                task.Project = projectDTOs.FirstOrDefault(p => p.Id == task.ProjectId);
                task.Employee = employeeDTOs.FirstOrDefault(p => p.Id == task.EmployeeId);
                task.Employee.Position = positionDTOs.FirstOrDefault(q => q.Id == task.Employee.PositionId);
                task.Status = statusDTOs.FirstOrDefault(p => p.Id == task.StatusId);
            }
            catch(Exception ex)
            {
                logger.LogError($"не удалось найти Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось найти задачу с таким ID в БД через EF");
            }
            return task;
        }

        public int Insert(TaskDTO objDTO)
        {
            try
            {
                taskDTOs.Add(objDTO);
                return context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось создать Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось добавить задачу в БД через EF");
            }
        }

        public void Update(TaskDTO objDTO)
        {
            try
            {
                taskDTOs.Update(objDTO);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось обновить Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось отредактировать задачу в БД через EF");
            }
        }

        public void Delete(int id)
        {
            try
            {
                TaskDTO task = taskDTOs.SingleOrDefault(s => s.Id == id);
                taskDTOs.Remove(task);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Task на уровне TaskDBRepository: {ex}");
                throw new Exception("Не удалось удалить задачу из БД через EF");
            }
        }
    }
}
