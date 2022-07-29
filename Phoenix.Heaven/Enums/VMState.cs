using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Phoenix.Heaven.Enums
{
    public enum VmState
    {
        [Description("Pending")]
        pending = 0,
        [Description("Running")]
        running = 16,
        [Description("Shutting-Down")]
        shuttingDown = 32,
        [Description("Terminated")]
        rerminated = 48,
        [Description("Stopping")]
        stopping = 64,
        [Description("Stopped")]
        stopped = 80       
    }
}
