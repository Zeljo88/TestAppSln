using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Extensions
{
    public static class Extensions
    {
        public static EC2Instance ToInstance(this List<Instance> instances)
        {
            var instance = new EC2Instance();

            foreach (var x in instances)
            {
                instance.InstanceId = x.InstanceId;
                instance.Name = x.Tags[0].Value;
                instance.State = x.State.Name;
                //instance.InstanceType = x.InstanceType;
            }
            return instance;
        }
    }
}
