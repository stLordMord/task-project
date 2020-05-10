using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public class ProjectReaderConverter: IReaderConverter<ProjectDTO>
    {
        public  List<ProjectDTO> converterToDTO(SqlDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            List<ProjectDTO> projects = new List<ProjectDTO>();
            while (reader.Read())
            {
                ProjectDTO project = new ProjectDTO()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    ShortName = Convert.ToString(reader["ShortName"]),
                    Description = Convert.ToString(reader["Description"])
                };
                projects.Add(project);
            }
            return projects;
        }
    }
}
