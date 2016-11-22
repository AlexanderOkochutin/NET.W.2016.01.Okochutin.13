using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    public interface ILogger
    {
        void Info(string message);
        void Trace(string message);
        void Debug(string message);
        void Error(string message);
        void Warn(string message);
    }
}
