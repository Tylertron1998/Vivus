using System;

namespace Vivus.Monitoring
{
    [Flags]
    public enum MonitorState
    {
        Running = 1 << 0,
        Stopped = 1 << 1,
        Paused = 1 << 2
    }
}