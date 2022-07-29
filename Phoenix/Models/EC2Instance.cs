using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Models
{
    public class EC2Instance
    {
        public string InstanceId { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string InstanceType { get; set; }

        public string IpAddress { get; set; }

        public string Cloud { get; set; }
    }
}
