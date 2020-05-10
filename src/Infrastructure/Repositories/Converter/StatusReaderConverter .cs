using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public class StatusReaderConverter : IReaderConverter<StatusDTO>
    {
        public  List<StatusDTO> converterToDTO(SqlDataReader reader)
        {

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            List<StatusDTO> statuses = new List<StatusDTO>();
            while (reader.Read())
            {
                StatusDTO status = new StatusDTO()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"])
                };
                statuses.Add(status);
            }
            return statuses;
        }
    }
}
