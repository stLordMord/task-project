using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ScalarConverter
    {
        // Scalar
        public static Func<SqlDataReader, List<int>> convert = converterScalar;
        static List<int> converterScalar(SqlDataReader reader)
        {
            if(reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            List<int> values = new List<int>();
            while (reader.Read())
            {
                var value = reader.GetInt32(0);
                values.Add(value);
            }
            return values;
        }
    }
}
