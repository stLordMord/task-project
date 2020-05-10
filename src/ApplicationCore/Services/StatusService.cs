using ApplicationCore;
using Common;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class StatusService : ISafeOperations<StatusBLO>
    {
        private readonly ISafeRepository<StatusDTO> statusRepository;
        private readonly IConverter<StatusDTO, StatusBLO> statusConverter;
        private readonly ILogger<StatusService> logger;

        public StatusService(ILogger<StatusService> logger, ISafeRepository<StatusDTO> statusRepository, IConverter<StatusDTO, StatusBLO> statusConverter)
        {
            this.logger = logger;
            this.statusRepository = statusRepository;
            this.statusConverter = statusConverter;
        }
        public int GetCount(string searchText)
        {
            int count;
            try
            {
                count = statusRepository.GetCount(searchText);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Statuses на уровне TaskDBRepository: {ex}");
                throw ex;
            }
            return count;
        }

        public IList<StatusBLO> GetAll(int page, int size, string searchText)
        {
            logger.LogDebug("Пользователь на слое ApplicationCore в сервисе StatusService");
            IList<StatusBLO> statuses = new List<StatusBLO>();
            try
            {
                statuses = statusConverter.Convert(statusRepository.GetAll(page, size, searchText));
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить все Statuses: {ex}");
                throw ex;
            }
            return statuses;
        }
        public StatusBLO GetById(int id)
        {
            StatusBLO status = new StatusBLO();
            try
            {
                status = statusConverter.Convert(statusRepository.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить Status: {ex}");
                throw ex;
            }
            return status;
        }
    }
}