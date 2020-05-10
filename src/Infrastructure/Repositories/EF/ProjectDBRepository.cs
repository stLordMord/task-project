using Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ProjectDBRepository : IRepository<ProjectDTO>
    {
        private readonly ILogger<IRepository<ProjectDTO>> logger;
        protected readonly TrainingContext context;
        private DbSet<ProjectDTO> projectDTOs;
        private DbSet<TaskDTO> taskDTOs;
        private DbSet<EmployeeDTO> employeeDTOs;
        private DbSet<StatusDTO> statusDTOs;
        private DbSet<PositionDTO> positionDTOs;

        public ProjectDBRepository(TrainingContext context, ILogger<IRepository<ProjectDTO>> logger)
        {
            this.logger = logger;
            this.context = context;
            projectDTOs = context.Projects;
            taskDTOs = context.Tasks;
            employeeDTOs = context.Employees;
            statusDTOs = context.Statuses;
            positionDTOs = context.Positions;
        }

        public int GetCount(string searchText)
        {
            logger.LogTrace("Метод GetCount на уровне ProjectDBRepository");
            try
            {
                int count = projectDTOs.Count(p => p.ShortName.Contains(searchText));
                return count;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количестов Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось получить количество проектов через EF");
            }
        }

        public IList<ProjectDTO> GetAll(int page, int size, string searchText)
        {
            logger.LogTrace("Метод GetAll на уровне ProjectDBRepository");
            IList<ProjectDTO> projects = new List<ProjectDTO>();
            try
            {
                return projects = projectDTOs.Where(p => p.ShortName.Contains(searchText))
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось получить список проектов через EF");
            }
        }

        public ProjectDTO GetById(int id)
        {
            logger.LogTrace("Метод GetById на уровне ProjectDBRepository");
            ProjectDTO project = new ProjectDTO();
            try
            {
                project = projectDTOs.FirstOrDefault(p => p.Id == id);
                project.Tasks = taskDTOs.Where(t => t.ProjectId == id).ToList();
                foreach (var task in project.Tasks)
                {
                    task.Employee = employeeDTOs.FirstOrDefault(p => p.Id == task.EmployeeId);
                    task.Employee.Position = positionDTOs.FirstOrDefault(q => q.Id == task.Employee.PositionId);
                    task.Status = statusDTOs.FirstOrDefault(p => p.Id == task.StatusId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось найти Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось найти проект с таким ID в БД через EF");
            }
            return project;
        }

        public int Insert(ProjectDTO objDTO)
        {
            try
            {
                projectDTOs.Add(objDTO);
                context.SaveChanges();
                return projectDTOs.LastOrDefault().Id;
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось создать Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось добавить проект в БД через EF");
            }
        }

        public void Update(ProjectDTO objDTO)
        {
            try
            {
                projectDTOs.Update(objDTO);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось обновить Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось отредактировать проект в БД через EF");
            }
        }

        public void Delete(int id)
        {
            try
            {
                ProjectDTO project = projectDTOs.SingleOrDefault(s => s.Id == id);
                projectDTOs.Remove(project);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Project на уровне ProjectDBRepository: {ex}");
                throw new Exception("Не удалось удалить проект из БД через EF так как у него есть задачи");
            }
        }

    }
}
