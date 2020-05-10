using ApplicationCore.Converter;
using Common;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class ProjectService : IService<ProjectBLO>
    {
        private readonly IRepository<ProjectDTO> projectRepository;
        private readonly IConverter<ProjectDTO, ProjectBLO> projectConverter;
        private readonly ILogger<ProjectService> logger;

        public ProjectService(ILogger<ProjectService> logger, IRepository<ProjectDTO> projectRepository, IConverter<ProjectDTO, ProjectBLO> projectConverter)
        {
            this.logger = logger;
            this.projectRepository = projectRepository;
            this.projectConverter = projectConverter;
        }

        public int GetCount(string searchText)
        {
            int count;
            try
            {
                count = projectRepository.GetCount(searchText);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количестов Project: {ex}");
                throw ex;
            }
            return count;
        }

        public IList<ProjectBLO> GetAll(int page, int size, string searchText)
        {
            logger.LogDebug("Пользователь на слое ApplicationCore в сервисе ProjectService");
            IList<ProjectBLO> projects = new List<ProjectBLO>();
            try
            {
                projects = projectConverter.Convert(projectRepository.GetAll(page, size, searchText));
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось получить список Project на уровне ProjectService");
                throw ex;
            }
            return projects;
        }
        public ProjectBLO GetById(int id)
        {
            ProjectBLO project = new ProjectBLO();
            try
            {
                project = projectConverter.Convert(projectRepository.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось найти Project на уровне ProjectService");
                throw ex;
            }
            return project;
        }

        public int Insert(ProjectBLO projectBLO)
        {
            int projectId;
            try
            {
                ProjectDTO project = new ProjectDTO();
                project = projectConverter.Convert(projectBLO);
                projectId = projectRepository.Insert(project);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось создать Project на уровне ProjectService");
                throw ex;
            }
            return projectId;
        }

        public void Update(ProjectBLO projectBLO)
        {
            try
            {
                ProjectDTO project = new ProjectDTO();
                project = projectConverter.Convert(projectBLO);
                projectRepository.Update(project);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Project на уровне ProjectDBRepository");
                throw ex;
            } 
        }

        public void Delete(int id)
        {
            try
            {
                projectRepository.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось удалить Project: {ex}");
                throw ex;
            }
        }
    }
}
