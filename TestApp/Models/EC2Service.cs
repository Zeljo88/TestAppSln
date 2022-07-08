using Amazon.EC2;
using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Extensions;
using TestApp.Services;

namespace TestApp.Models
{
    public class EC2Service : IEC2Service
    {
        private readonly IAmazonEC2 _client;
        public EC2Service(IAmazonEC2 client)
        {
            _client = client;

        }

        public async Task<EC2Response> CreateInstanceAsync(string amiID)
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


        public Task<EC2Response> TerminateInstanceAsync(string instanceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EC2Instance>> GetAllInstancesAsync()
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


            var inst = (from reservation in response.Reservations
                       where reservation.Instances.Count != 0
                       select reservation.Instances.ToInstance()).ToList();           

            return inst;
        }

        public async Task<InstanceState> StartInstanceAsync(string instanceId)
        {
            var response = await _client.StartInstancesAsync(new StartInstancesRequest(new List<string> { instanceId }));

            return response.StartingInstances[0].CurrentState;
        }

        public async Task<InstanceState> StopInstanceAsync(string instanceId)
        {
            var response = await _client.StopInstancesAsync(new StopInstancesRequest(new List<string> { instanceId }));

            return response.StoppingInstances[0].CurrentState;
        }
    }
}
