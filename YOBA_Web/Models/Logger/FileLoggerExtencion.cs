using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOBA_Web.Models.Logger
{
    /// <summary>
    /// Extencion method for ILoggerFactory
    /// to add LoggerFactory as text file
    /// </summary>
    public static class FileLoggerExtencion
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
