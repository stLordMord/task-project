using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public class TaskReaderConverter: IReaderConverter<TaskDTO>
    {
        public  List<TaskDTO> converterToDTO(SqlDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            List<TaskDTO> tasks = new List<TaskDTO>();
            while (reader.Read())
            {
                TaskDTO task = new TaskDTO()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ProjectId = Convert.ToInt32(reader["ProjectId"]),
                    Name = Convert.ToString(reader["Name"]),
                    Timing = Convert.ToInt32(reader["Timing"]),
                    DateStart = Convert.ToDateTime(reader["DateStart"]),
                    DateEnd = Convert.ToDateTime(reader["DateEnd"]),
                    StatusId = Convert.ToInt32(reader["StatusId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"])
                };
                tasks.Add(task);
            }
            return tasks;
        }
    }
}
