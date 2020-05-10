using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected abstract string ConnectionString { get; }
        private readonly ILogger logger;

        protected BaseRepository(ILogger logger)
        {
            this.logger = logger;
        }

        
        protected List<U> Execute<U>(string query, List<SqlParameter> list, Func<SqlDataReader, List<U>> convert)
        {
            if (query == null)
            {
                logger.LogError("Пустая строка запроса");
                throw new ArgumentNullException(nameof(query));
            }
            if (list == null)
            {
                logger.LogError("Пустая список параметров");
                throw new ArgumentNullException(nameof(list));
            }
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand(query, sqlCon);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(list.ToArray());
                    SqlDataReader reader = command.ExecuteReader();
                    return convert(reader);
                }
            }
            catch
            {
                throw new Exception("Отсутствует соединение с базой данных");
            }
        }

        protected void ExecuteNonQuery(string query, List<SqlParameter> list)
        {
            if (query == null)
            {
                logger.LogError("Пустая строка запроса");
                throw new ArgumentNullException(nameof(query));
            }
            if (list == null)
            {
                logger.LogError("Пустая список параметров");
                throw new ArgumentNullException(nameof(list));
            }
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand(query, sqlCon);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(list.ToArray());
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Отсутствует соединение с базой данных");
            }
        }
    }
}
