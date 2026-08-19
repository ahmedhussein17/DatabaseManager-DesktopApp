using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DatabaseManager.Models;

namespace DatabaseManager.Services
{
    public static class ConfigService
    {
        private static readonly string ConfigPath = "Config/connections.json";

        public static List<ConnectionInfo> LoadConnections()
        {
            string json = File.ReadAllText(ConfigPath);
            var connections = JsonSerializer.Deserialize<List<ConnectionInfo>>(json);
            return connections ?? new List<ConnectionInfo>();
        }
    }
}