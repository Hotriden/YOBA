using Microsoft.Extensions.Logging;

namespace YOBA_Web.Models.Logger
{
    /// <summary>
    /// Logger provider create filelogger
    /// on cunstructor instance
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        public FileLoggerProvider(string _path)
        {
            path = _path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
