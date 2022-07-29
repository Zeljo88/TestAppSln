using System;
using System.Collections.Generic;
using System.Text;
using Phoenix.Heaven.Enums;
using Phoenix.Heaven.Interfaces;

namespace Phoenix.Heaven.Models
{
    public class VM : IVMable
    {
        public string VmId { get; set; }
        public VmState Status { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public string VMType { get; set; }
        public string Tags { get; set; }
        public string IpAddress { get; set; }
        public string Cloud { get; set; }
        public IResourceGroupable Parent { get; set; }

        public bool Restart()
        {
            throw new NotImplementedException();
        }

        public bool Start()
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
