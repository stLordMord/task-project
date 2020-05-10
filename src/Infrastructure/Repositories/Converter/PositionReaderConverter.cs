using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public class PositionReaderConverter : IReaderConverter<PositionDTO>
    {
        public  List<PositionDTO> converterToDTO(SqlDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            List<PositionDTO> positions = new List<PositionDTO>();
            while (reader.Read())
            {
                PositionDTO position = new PositionDTO()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"])
                };
                positions.Add(position);
            }
            return positions;
        }
    }
}
