using Common;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class TaskService : IService<TaskBLO>
    {
        private readonly IRepository<TaskDTO> taskRepository;
        private readonly IConverter<TaskDTO, TaskBLO> taskConverter;
        private readonly ILogger<TaskService> logger;

        public TaskService(ILogger<TaskService> logger, IRepository<TaskDTO> taskRepository, IConverter<TaskDTO, TaskBLO> taskConverter)
        {
            this.logger = logger;
            this.taskRepository = taskRepository;
            this.taskConverter = taskConverter;
        }
        public int GetCount(string searchText)
        {
            int count;
            try
            {
                count = taskRepository.GetCount(searchText);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Task {ex}");
                throw ex;
            }
            return count;
        }
        public IList<TaskBLO> GetAll(int page, int size, string searchText)
        {
            logger.LogTrace("Метод GetAll() на слое ApplicationCore в сервисе TaskService");
            IList<TaskBLO> tasks = new List<TaskBLO>();
            try
            {
                tasks = taskConverter.Convert(taskRepository.GetAll(page, size, searchText));
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить список Task на уровне TaskService");
                throw ex;
            }
            return tasks;
        }
        public TaskBLO GetById(int id)
        {
            logger.LogTrace("Метод GetAll() на слое ApplicationCore в сервисе TaskService");
            TaskBLO task = new TaskBLO();
            try
            {
                task = taskConverter.Convert(taskRepository.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogError($"не удалось найти Task на уровне TaskService");
                throw ex;
            }
            return task;
        }

        public int Insert(TaskBLO taskBLO)
        {
            logger.LogTrace("Метод GetAll() на слое ApplicationCore в сервисе TaskService");
            int taskId;
            try
            {
                TaskDTO task = new TaskDTO();
                task = taskConverter.Convert(taskBLO);
                taskId = taskRepository.Insert(task);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось создать Task на уровне TaskService");
                throw ex;
            }
            return taskId;
        }

        public void Update(TaskBLO taskBLO)
        {
            logger.LogTrace("Метод GetAll() на слое ApplicationCore в сервисе TaskService");
            try
            {
                TaskDTO task = new TaskDTO();
                task = taskConverter.Convert(taskBLO);
                taskRepository.Update(task);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось обновить Task на уровне TaskService");
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                logger.LogTrace("Метод GetAll() на слое ApplicationCore в сервисе TaskService");
                taskRepository.Delete(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
