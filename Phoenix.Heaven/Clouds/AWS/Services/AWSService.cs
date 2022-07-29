using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Extensions;
using Phoenix.Heaven.Clouds.AWS.Models;
using Phoenix.Heaven.Interfaces;
using Phoenix.Heaven.Models;

namespace Phoenix.Heaven.Clouds.AWS.Services
{
    public class AWSService : IAWSService
    {
        private readonly IAmazonEC2 _client;

        private readonly IAmazonS3 _s3client;

        private readonly IAmazonCloudFormation _cfnClient;
        public AWSService(IAmazonEC2 client, IAmazonCloudFormation cfnClient, IAmazonS3 s3client)
        {
            _client = client;
            _cfnClient = cfnClient;
            _s3client = s3client;

        }

        public async Task<EC2Response> CreateVMAsync(string amiID)
        {

            var createRequest = new CreateSecurityGroupRequest
            {
                GroupName = "testSecGroup",
                Description = "My sample security group for EC2-Classic"
            };

            var createResponse = await _client.CreateSecurityGroupAsync(createRequest);


            var Groups = new List<string>() {
               createResponse.GroupId
              };
            var describeRequest = new DescribeSecurityGroupsRequest()
            {
                GroupIds = Groups
            };
            var describeResponse = await _client.DescribeSecurityGroupsAsync(describeRequest);

            IpRange ipRange = new IpRange
            {
                CidrIp = "1.1.1.1/1"
            };
            List<IpRange> ranges = new List<IpRange> {
                ipRange
            };

            var ipPermission = new IpPermission
            {
                IpProtocol = "tcp",
                FromPort = 22,
                ToPort = 22,
                Ipv4Ranges = ranges
            };

            var ingressRequest = new AuthorizeSecurityGroupIngressRequest
            {
                GroupId = describeResponse.SecurityGroups[0].GroupId
            };
            ingressRequest.IpPermissions.Add(ipPermission);
            var ingressResponse = await _client.AuthorizeSecurityGroupIngressAsync(ingressRequest);

            var request = new CreateKeyPairRequest
            {
                KeyName = "testKeyPair"
            };

            var response = await _client.CreateKeyPairAsync(request);
            Console.WriteLine();
            Console.WriteLine("New key: " + "testKeyPair");

            // Save the private key in a .pem file    
            using (FileStream s = new FileStream("privatekeyFike.pem", FileMode.Create))
            using (StreamWriter writer = new StreamWriter(s))
            {
                writer.WriteLine(response.KeyPair.KeyMaterial);
            }
            string keyPairName = "testKeyPair";

            List<string> groups = new List<string>() {
                    describeResponse.SecurityGroups[0].GroupId
                };
            var launchRequest = new RunInstancesRequest()
            {
                ImageId = amiID,
                InstanceType = InstanceType.T2Micro,
                MinCount = 1,
                MaxCount = 1,
                KeyName = keyPairName,
                SecurityGroupIds = groups,
            };

            var launchResponse = await _client.RunInstancesAsync(launchRequest);

            return new EC2Response
            {
                Message = launchResponse.ResponseMetadata.RequestId,
                Status = launchResponse.HttpStatusCode
            };
        }


        public Task<VM> TerminateVMAsync(string instanceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VM>> GetAllVMsAsync()
        {
            var response = await _client.DescribeInstancesAsync(new DescribeInstancesRequest
            {
                MaxResults = 1000
                //   Filters = new List<Filter> {
                //        new Filter {
                //   Name = "instance-type",
                //   Values = new List<string> {
                //   "t2.micro"
                //}
            });


            var vm = (from reservation in response.Reservations
                        where reservation.Instances.Count != 0
                        select reservation.Instances.ToVM()).ToList();

            return vm;
        }

        public async Task<InstanceState> StartVMAsync(string instanceId)
        {
            var response = await _client.StartInstancesAsync(new StartInstancesRequest(new List<string> { instanceId }));

            return response.StartingInstances[0].CurrentState;
        }

        public async Task<InstanceState> StopVMAsync(string instanceId)
        {
            var response = await _client.StopInstancesAsync(new StopInstancesRequest(new List<string> { instanceId }));

            return response.StoppingInstances[0].CurrentState;
        }

        public async Task<List<StackResource>> ListResources()
        {
            Console.WriteLine("Getting CloudFormation stack information...");
            var responseDescribeStacks = await _cfnClient.DescribeStacksAsync();
            DescribeStackResourcesResponse responseDescribeResources = new DescribeStackResourcesResponse();
            foreach (Stack stack in responseDescribeStacks.Stacks)
            {
                // Basic information for each stack
                Console.WriteLine("\n------------------------------------------------");
                Console.WriteLine($"\nStack: {stack.StackName}");
                Console.WriteLine($"  Status: {stack.StackStatus.Value}");
                Console.WriteLine($"  Created: {stack.CreationTime}");

                // The tags of each stack (etc.)
                if (stack.Tags.Count > 0)
                {
                    Console.WriteLine("  Tags:");
                    foreach (Amazon.CloudFormation.Model.Tag tag in stack.Tags)
                        Console.WriteLine($"    {tag.Key}, {tag.Value}");
                }

                // The resources of each stack
                responseDescribeResources =
                await _cfnClient.DescribeStackResourcesAsync(new DescribeStackResourcesRequest
                {
                    StackName = stack.StackName
                });
                if (responseDescribeResources.StackResources.Count > 0)
                {
                    Console.WriteLine("  Resources:");
                    foreach (StackResource resource in responseDescribeResources.StackResources)
                        Console.WriteLine($"    {resource.LogicalResourceId}: {resource.ResourceStatus}");
                }

            }
            Console.WriteLine("\n------------------------------------------------");
            return responseDescribeResources.StackResources;
        }

        public async Task<List<TemplateParameter>> ValidateTemplate()
        {
            var response = await _cfnClient.ValidateTemplateAsync(new ValidateTemplateRequest
            {
                TemplateBody = "MyTemplate.json"
            });

            List<string> capabilities = response.Capabilities;
            string capabilitiesReason = response.CapabilitiesReason;
            string description = response.Description;
            List<TemplateParameter> parameters = response.Parameters;
            return parameters;
        }

        public async Task<GetTemplateResponse> GetTemplateAsync(GetTemplateRequest request, CancellationToken cancellationToken)
        {
            var template = await _cfnClient.GetTemplateAsync(request);

            return template;
        }

        public async Task<CreateStackResponse> CreateStackAsync()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            var request = new CreateStackRequest
            {
                TemplateURL = "https://cf-templates-1ec1lh578v1v8-eu-central-1.s3.eu-central-1.amazonaws.com/Windows%20ADServer?response-content-disposition=inline&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELP%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCmV1LW5vcnRoLTEiRjBEAiBziJ62NikwNEKnDxOWiYTuLO5f5X6okrtqLNVUdSI8mQIgaKT7aWNruYKNnveHo3GEuU7kIHHfes%2BfBHr0mvtZN%2Fwq7QII7P%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FARAAGgw3OTUwODAzMTc0MjkiDDR1%2BTLJIQNsJJuseCrBAv4oUFo1aOiTNm4oFCQqQL0PBNBdE7P5%2F4MDkI74zWK2eYYJrfzFX2n7gPsEjYhT76Wuyzs6Rni47Ss6Ug91mthCrCUZrmSGoMjBzmcaY2FQiJQDfom2ChM9%2FO7bnLvRWZmgdQtuV8VChc35fzGsNDiwVsnGalV4ES%2BRro93KlQbavCn13fxbfqZ%2B425GxB0nZVpaH5KcV862srXWRuBNl2tl4sAMoWK1SrmxG1gQ91Y7%2FWUDGtpNMIKB0YWRtlK5KDs1rvFNBN0jH4%2BP1jSR660HbqcmMMoiaSYXWOQe3K68fyjDdYv2Ft71ZXClTqesMamxSzM4zFMvZy0ThQMoJA4lzQj%2Beu7iIX%2Ba5KkaniFps3HagCRsCkbZDO8c6dZIqQ2yAExg%2FIqDZSMg1DfHQa4K4CuTiYY%2B%2BOwkjgMGY7x3jCmk9SWBjq0AqivBW8XlSb5CXB0lgnAagRYONGeqY%2BBsDO2IhePrCwI1XSOQ3c5C7ZnMOJWrGgoVcSDr%2BT7YO5r9M3Q%2Fyu%2B1K0QcmDDWcV2ajsxxjr9MFUjtpjdIrZDW26lcxu8Odn%2B%2FON9r1RzVqdvn%2F1ApT85xSI9YhLVWK3FoSG6xLyYOjPKsYe%2FzsYZPDpcRDKEdo8aZwNHjUEDjoJu4BoYw7iVQVsLXzaxY2yVBs59jaOtX7m53U6ysTBdyA%2BEu4AGM3tCnJEhLxFTKzyMfK1LlTJ%2F5aj79j7bnaOwQ6C1zaIFXDjZ2lxsZO92Gaege6qKJpjzlAmlBCezoAvDc7JvkCNNcorrbJIyBPpNOjbx4lrYPlqLJzaSUa0PYqcYao%2FsYj95s2zfJiXoIX2lkAiDGQuEh4JnXhDW&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20220718T121526Z&X-Amz-SignedHeaders=host&X-Amz-Expires=18000&X-Amz-Credential=ASIA3SHT23H2WMGIEH6U%2F20220718%2Feu-central-1%2Fs3%2Faws4_request&X-Amz-Signature=7a25e2f612448979a10c059c8d37b6f3dd36c4666be067936cda83a45c6e1067",
                StackName = "WindowsAdStack" + rnd.Next().ToString(),

            };
            var response = await _cfnClient.CreateStackAsync(request);

            return response;
        }

        /// <summary>
        /// Uses the client object to get a list of the objects in the Amazon
        /// S3 bucket in the bucketName parameter.
        /// </summary>       
        /// <param name="bucketName">The bucket name for which you want to
        /// retrieve a list of objects.</param>
        public async Task<List<Template>> ListingObjectsAsync()
        {
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request()
                {
                    BucketName = "getbucket-ybda717nr8z754oj5dorzujujqqs1euc1a-s3alias",
                    MaxKeys = 5,

                };

                var response = new ListObjectsV2Response();
                var templateList = new List<Template>();

                response = await _s3client.ListObjectsV2Async(request);

                response.S3Objects
                      .ForEach(obj => Console.WriteLine($"{obj.Key,-35}{obj.LastModified.ToShortDateString(),10}{obj.Size,10}"));



                foreach (var x in response.S3Objects)
                {

                    var objTemplate = await _s3client.GetObjectAsync(x.BucketName, x.Key);

                    var template = new Template
                    {
                        Id = objTemplate.Metadata.Keys.ToString(),
                        Name = x.Key,
                    };

                    var getUrlrequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = x.BucketName,
                        Key = x.Key,
                        Expires = DateTime.UtcNow.AddHours(1),
                    };

                    template.Url = _s3client.GetPreSignedURL(getUrlrequest);

                    templateList.Add(template);
                }

                return templateList;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' getting list of objects.");
                return null;
            }

        }

     
    }

}
