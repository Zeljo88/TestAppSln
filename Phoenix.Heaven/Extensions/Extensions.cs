using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using Phoenix.Heaven.Models;

namespace Phoenix.Extensions
{
    public static class Extensions
    {
        public static VM ToVM(this List<Instance> instances)
        {
            var vm = new VM();

            foreach (var x in instances)
            {
                vm.VmId = x.InstanceId;
                vm.Name = x.Tags[0].Value;
                vm.Status = (Heaven.Enums.VmState)x.State.Code;
                vm.VMType = x.InstanceType.Value;
                vm.IpAddress = x.PublicIpAddress == null ? "N/A" : x.PublicIpAddress;
                vm.Cloud = "AWS";                 
            }
            return vm;
        }
    }
}
