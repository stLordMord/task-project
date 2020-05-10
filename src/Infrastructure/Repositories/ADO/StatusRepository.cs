using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Repositories.Converter;

namespace Infrastructure.Repositories
{
    public class StatusRepository : BaseRepository<StatusDTO>, ISafeRepository<StatusDTO>
    {
        private readonly ILogger<ISafeRepository<StatusDTO>> logger;
        protected override string ConnectionString { get; }
        public StatusRepository(string connectionString, 
            ILogger<ISafeRepository<StatusDTO>> logger,
            IReaderConverter<StatusDTO> statusConverter) : base(logger)
        {
            this.logger = logger;
            ConnectionString = connectionString;
            convert = statusConverter.converterToDTO;
        }

        private readonly Func<SqlDataReader, List<StatusDTO>> convert;

        public int GetCount(string searchText)
        {
            try
            {
                string query = "GetCountOfStatuses";
                List<SqlParameter> parameters = new List<SqlParameter>();
                var count = Execute<int>(query, parameters, ScalarConverter.convert);
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить количество Statuses на уровне StatusRepository: {ex}");
                throw new Exception("Не удалось получить количество статусов");
            }
        }

        public IList<StatusDTO> GetAll(int page, int size, string searchText)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "GetAllStatuses";
                IList<StatusDTO> statuses = Execute(query, parameters, convert);
                return statuses;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить все Statuses на уровне StatusDepository: {ex}");
                throw new Exception("Не удалось получить список статусов в БД");
            }
        }

        public StatusDTO GetById(int id)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@Id", id));
                string query = "GetStatusById";
                List<StatusDTO> statuses = Execute(query, list, convert);
                StatusDTO status = new StatusDTO();
                status = statuses.FirstOrDefault();
                return status;
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось получить Status на уровне StatusRepository: {ex}");
                throw new Exception("Не удалось найти статус с таким ID в БД");
            }
        }
    }
}
