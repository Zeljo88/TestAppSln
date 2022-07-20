using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TestApp.Heaven.Enums
{
    public enum VmState
    {
        [Description("Stopped")]
        Stopped = 1,
        [Description("Starting")]
        Starting = 2,
        [Description("Running")]
        Running = 3,
        [Description("Stopping")]
        Stopping = 4,
        [Description("Deallocating")]
        Deallocating = 5,
        [Description("Deallocated")]
        Deallocated = 6
    }
}
