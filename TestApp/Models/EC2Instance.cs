using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class EC2Instance
    {
        public string InstanceId { get; set; }

        public string Name { get; set; }

        public string State { get; set; }
    }
}
