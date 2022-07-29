using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Phoenix.Heaven.Enums
{
    public enum ResourceType
    {
        [Description("Virtual Network")]
        VirtualNetwork = 1,
        [Description("Virtual Machine")]
        VirtualMachine = 2,
        [Description("Disk")]
        Disk = 3,
        [Description("IP Address")]
        IpAddress = 4,
        [Description("Network security group")]
        NetworkSecurityGroup = 5,
        [Description("Network interface")]
        NetworkInterface = 6
    }
}
