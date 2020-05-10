using Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PositionDBRepository : ISafeRepository<PositionDTO>
    {
        private readonly ILogger<ISafeRepository<PositionDTO>> logger;
        private readonly TrainingContext context;
        private DbSet<PositionDTO> positionDTOs;

        public PositionDBRepository(TrainingContext context, ILogger<ISafeRepository<PositionDTO>> logger)
        {
            this.logger = logger;
            this.context = context;
            positionDTOs = context.Positions;
        }

        public int GetCount(string searchText)
        {
            try
            {
                int count = positionDTOs.Count();
                return count;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Pоsitions { ex.ToString()}");
                throw new Exception("Не удалось получить количество должностей через EF");
            }
        }

        public IList<PositionDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                var positions = positionDTOs.ToList();
                return positions;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Psitions { ex.ToString()}");
                throw new Exception("Не удалось получить список должностей из БД через EF");
            }
        }

        public PositionDTO GetById(int id)
        {
            try
            {
                var position = positionDTOs.FirstOrDefault(p => p.Id == id);
                return position;
            }
            catch(Exception ex)
            {
                logger.LogError($"Не удалось получить Position { ex.ToString()}");
                throw new Exception("Не удалось найти должность с таким ID через EF");
            }
        }
    }
}
