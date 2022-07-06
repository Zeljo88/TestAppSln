using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services
{
    public interface IEC2Service
    {
        Task<EC2Response> CreateInstanceAsync(string amiID);
        Task<EC2Response> TerminateInstanceAsync(string instanceId);

        Task<List<EC2Instance>> GetAllInstancesAsync();
    }
}
