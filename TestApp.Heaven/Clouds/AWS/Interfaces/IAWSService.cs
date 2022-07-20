using Amazon.CloudFormation.Model;
using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Heaven.Clouds.AWS.Models;
using TestApp.Heaven.Models;

namespace TestApp.Heaven.Interfaces
{
   public interface IAWSService
    {
        Task<EC2Response> CreateVMAsync(string amiID);
        Task<VM> TerminateVMAsync(string vmId);

        Task<List<VM>> GetAllVMsAsync();

        Task<InstanceState> StartVMAsync(string instanceId);

        Task<InstanceState> StopVMAsync(string instanceId);

        Task<List<StackResource>> ListResources();

        Task<CreateStackResponse> CreateStackAsync();

        Task<List<Template>> ListingObjectsAsync();
    }
}
