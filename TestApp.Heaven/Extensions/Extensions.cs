using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using TestApp.Heaven.Models;

namespace TestApp.Extensions
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
                vm.Type = (Heaven.Enums.ResourceType)Convert.ToInt32(x.InstanceType.Value);
            }
            return vm;
        }
    }
}
