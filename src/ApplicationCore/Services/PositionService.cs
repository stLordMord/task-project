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
    public class PositionService : ISafeOperations<PositionBLO>
    {
        private readonly ISafeRepository<PositionDTO> positionRepository;
        private readonly IConverter<PositionDTO, PositionBLO> positionConverter;
        private readonly ILogger<PositionService> logger;

        public PositionService(ILogger<PositionService> logger, ISafeRepository<PositionDTO> positionRepository, IConverter<PositionDTO, PositionBLO> positionConverter)
        {
            this.logger = logger;
            this.positionRepository = positionRepository;
            this.positionConverter = positionConverter;
        }
        public int GetCount(string searchText)
        {
            int count;
            try
            {
                count = positionRepository.GetCount(searchText);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Pоsitions { ex.ToString()}");
                throw ex;
            }
            return count;
        }

        public IList<PositionBLO> GetAll(int page, int size, string searchText)
        {
            logger.LogDebug("Пользователь на слое ApplicationCore в сервисе PositionService");
            IList<PositionBLO> positions = new List<PositionBLO>();
            try
            {
                positions = positionConverter.Convert(positionRepository.GetAll(page, size, searchText));
                
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Positions { ex.ToString()}");
                throw ex;
            }
            return positions;
        }
        public PositionBLO GetById(int id)
        {
            PositionBLO position = new PositionBLO();
            try
            {
                position = positionConverter.Convert(positionRepository.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить Position { ex.ToString()}");
                throw ex;
            }
            return position;
        }
    }
}