using System.Collections.Generic;
using System.Linq;

namespace Vivus.Logging
{
    public class Logger
    {
        private List<LogEntry> _logs;

        public void Log(string message, LogLevel level = LogLevel.Log)
        {
            _logs.Add(new LogEntry(message, level));
        }

        public IEnumerable<string> GetLogs(LogLevel level = LogLevel.Log)
        {
            return _logs
                .Where(log => (log.LogLevel & level) == 0)
                .Select(entry => entry.Message);
        }
        private struct LogEntry
        {
            public string Message { get; set; } 
            public LogLevel LogLevel { get; set; }

            public LogEntry(string message, LogLevel level)
            {
                Message = message;
                LogLevel = level;
            } 
        }
    }
}