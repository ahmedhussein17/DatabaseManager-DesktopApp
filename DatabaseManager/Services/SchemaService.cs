using Microsoft.Data.SqlClient;
using DatabaseManager.Models;

namespace DatabaseManager.Services
{
    public static class SchemaService
    {
        public static List<DbObjectInfo> GetDatabaseObjects(string connectionString)
        {
            var results = new List<DbObjectInfo>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT name, type_desc
                    FROM sys.objects
                    WHERE type IN ('U', 'V', 'P')
                    ORDER BY type_desc, name";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new DbObjectInfo
                        {
                            Name = reader.GetString(0),
                            Type = reader.GetString(1)
                        });
                    }
                }
            }

            return results;
        }
    }
}