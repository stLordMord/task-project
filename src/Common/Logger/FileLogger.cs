using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Logger
{
    public class FileLogger<TCategoryName> : ILogger<TCategoryName>
    {
        private string pathFile;
        public FileLogger(string path)
        {
            pathFile = Path.Combine(path, $"log-{DateTime.Now.ToString("MM.dd.yyyy")}.txt");
            using (StreamWriter sw = new StreamWriter(File.Open(pathFile, FileMode.Append))) { };
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel}|{DateTime.Now}|{formatter(state, exception)}\n";
            File.AppendAllText(pathFile, message);
        }
    }
}
