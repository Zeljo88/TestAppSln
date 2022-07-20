using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.EC2.Model;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services
{
    public interface IVMService
    {
        Task<EC2Response> CreateVMAsync(string amiID);
        Task<EC2Response> TerminateInstanceAsync(string instanceId);

        Task<List<EC2Instance>> GetAllInstancesAsync();

        Task<InstanceState> StartInstanceAsync(string instanceId);

        Task<InstanceState> StopInstanceAsync(string instanceId);

        Task<List<StackResource>> ListResources();

        Task<CreateStackResponse> CreateStackAsync();

        Task<List<Template>> ListingObjectsAsync();
    }
}
