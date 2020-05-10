using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories.Converter
{
    public interface IReaderConverter<T>
    {
        List<T> converterToDTO(SqlDataReader reader);
    }
}
