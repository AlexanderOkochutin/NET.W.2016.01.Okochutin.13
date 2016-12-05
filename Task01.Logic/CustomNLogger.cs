using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task01.Logic
{
    public class CustomNLogger:ILogger
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message) => logger.Debug(message);

        public void Error(string message) => logger.Error(message);

        public void Info(string message) => logger.Info(message);
        
        public void Trace(string message) => logger.Trace(message);

        public void Warn(string message) => logger.Warn(message);
    }
}
