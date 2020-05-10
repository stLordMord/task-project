using Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class StatusDBRepository : ISafeRepository<StatusDTO>
    {
        private readonly ILogger<ISafeRepository<StatusDTO>> logger;
        private readonly TrainingContext context;
        private DbSet<StatusDTO> statusDTOs;

        public StatusDBRepository(TrainingContext context, ILogger<ISafeRepository<StatusDTO>> logger)
        {
            this.logger = logger;
            this.context = context;
            statusDTOs = context.Statuses;
        }
        public int GetCount(string searchText)
        {
            try
            {
                int count = statusDTOs.Count();
                return count;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Statuses на уровне StatusDBRepository: {ex}");
                throw new Exception("Не удалось получить количество статусов через EF");
            }

        }
        public IList<StatusDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                var statuses = statusDTOs.ToList();
                return statuses;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить все Statuses на уровне StatusDBRepository: {ex}");
                throw new Exception("Не удалось получить список статусов в БД через EF");
            }
        }

        public StatusDTO GetById(int id)
        {
            try
            {
                var status = statusDTOs.FirstOrDefault(p => p.Id == id);
                return status;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить Status на уровне StatusDBRepository: {ex}");
                throw new Exception("Не удалось найти статус с таким ID в БД через EF");
            }
        }
    }
}
