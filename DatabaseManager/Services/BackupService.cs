using Microsoft.Data.SqlClient;

namespace DatabaseManager.Services
{
    public static class BackupService
    {
        // returns: (success, message, backupName)
        public static (bool Success, string Message, string BackupName) BackupTable(string connectionString, string tableName)
        {
            string backupName = tableName + "_" + DateTime.Now.ToString("yyyyMMdd");

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM sys.tables WHERE name = @backupName";
                using (var checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@backupName", backupName);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                        return (false, "Backup already exists: " + backupName, backupName);
                }

                string backupQuery = $"SELECT * INTO [{backupName}] FROM [{tableName}]";
                using (var backupCmd = new SqlCommand(backupQuery, conn))
                {
                    backupCmd.ExecuteNonQuery();
                }

                return (true, "Backup created: " + backupName, backupName);
            }
        }
    }
}