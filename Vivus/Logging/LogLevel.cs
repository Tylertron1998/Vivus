using System;

namespace Vivus.Logging
{
    [Flags]
    public enum LogLevel
    {
        Error = 1 << 0,
        Warning = 1 << 1,
        Debug = 1 << 2,
        Log = 1 << 3,
        All = Error | Warning | Debug | Log
    }
}