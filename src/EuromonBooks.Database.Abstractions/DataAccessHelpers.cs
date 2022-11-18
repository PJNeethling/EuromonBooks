using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using System.Data;

namespace EuromonBooks.Database.Abstractions
{
    public static class DataAccessHelpers
    {
        public static string GetParameterNames(SqlParameter[] parameters)
        {
            return string.Join(", ", parameters.Select(x => x.ParameterName));
        }

        public static object GetIdListSqlDataRecords(List<int> data)
        {
            if (data == null || data.Count == 0)
            {
                return null;
            }

            var sqlMetaData = new List<SqlMetaData>
            {
                new SqlMetaData("Id", SqlDbType.Int)
            };

            var dataTable = new List<SqlDataRecord>();

            foreach (var item in data)
            {
                SqlDataRecord row = new(sqlMetaData.ToArray());
                row.SetValues(item);
                dataTable.Add(row);
            }

            return dataTable;
        }
    }
}