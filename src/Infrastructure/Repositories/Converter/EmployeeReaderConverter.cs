using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public class EmployeeReaderConverter: IReaderConverter<EmployeeDTO>
    {
        public  List<EmployeeDTO> converterToDTO(SqlDataReader reader)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            while (reader.Read())
            {
                if (reader == null)
                {
                    throw new ArgumentNullException(nameof(reader));
                }
                EmployeeDTO employee = new EmployeeDTO()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    Surname = Convert.ToString(reader["Surname"]),
                    Patronymic = Convert.ToString(reader["Patronymic"]),
                    PositionId = Convert.ToInt32(reader["PositionId"])
                };
                employees.Add(employee);
            }
            return employees;
        }
    }
}
