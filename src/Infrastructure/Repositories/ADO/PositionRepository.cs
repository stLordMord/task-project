using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Repositories.Converter;

namespace Infrastructure.Repositories
{
    public class PositionRepository : BaseRepository<PositionDTO>, ISafeRepository<PositionDTO>
    {
        private readonly ILogger<ISafeRepository<PositionDTO>> logger;
        protected override string ConnectionString { get; }
        public PositionRepository(string connectionString, 
            ILogger<ISafeRepository<PositionDTO>> logger,
            IReaderConverter<PositionDTO> positionConverter) : base(logger)
        {
            this.logger = logger;
            ConnectionString = connectionString; 
            convert = positionConverter.converterToDTO;
        }

        private readonly Func<SqlDataReader, List<PositionDTO>> convert;

        public int GetCount(string searchText)
        {
            try
            {
                string query = "GetCountOfPositions";
                List<SqlParameter> parameters = new List<SqlParameter>();
                var count = Execute<int>(query, parameters, ScalarConverter.convert);
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Pоsitions { ex.ToString()}");
                throw new Exception("Не удалось получить количество должностей");
            }
        }

        public IList<PositionDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "GetAllPositions";
                IList<PositionDTO> positions = Execute(query, parameters, convert);
                return positions;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить список Psitions { ex.ToString()}");
                throw new Exception("Не удалось получить список должностей из БД");
            }
        }

        public PositionDTO GetById(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", id));
                string query = "GetPositionById";
                List<PositionDTO> positions = Execute(query, parameters, convert);
                PositionDTO position = new PositionDTO();
                position = positions.FirstOrDefault();
                return position;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить Position { ex.ToString()}");
                throw new Exception("Не удалось найти должность с таким ID");
            }
        }
    }
}
