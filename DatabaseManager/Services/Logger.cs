using System;
using System.IO;

namespace DatabaseManager.Services
{
    public static class Logger
    {
        private static readonly string LogPath = "Logs/log.txt";

        public static void Log(string operation, string source, string destination, string result)
        {
            Directory.CreateDirectory("Logs"); //makes sure the folder exists
            string line = $"{DateTime.Now}, {operation}, {source}, {destination}, {result}";
            File.AppendAllText(LogPath, line + Environment.NewLine);
        }
    }
}