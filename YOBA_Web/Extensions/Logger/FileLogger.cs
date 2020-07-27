using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace YOBA_Web.Models.Logger
{
    /// <summary>
    /// Main implementation of logger
    /// instance
    /// </summary>
    public class FileLogger : ILogger
    {
        private string filePath;
        /// <summary>
        /// log file locker
        /// </summary>
        private static object _lock = new object();

        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
