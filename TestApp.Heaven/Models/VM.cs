using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Heaven.Enums;
using TestApp.Heaven.Interfaces;

namespace TestApp.Heaven.Models
{
    public class VM : IVMable
    {
        public string VmId { get; set; }
        public VmState Status { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public string Tags { get; set; }
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
