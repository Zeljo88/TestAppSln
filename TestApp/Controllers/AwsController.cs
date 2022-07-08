using Amazon.S3;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AwsController : ControllerBase
    {
        private readonly IEC2Service _service;

        public AwsController(IEC2Service service)
        {
            _service = service;
        }
        [HttpPost("{amiID}")]
        public async Task<IActionResult> CreateEC2Instance([FromRoute] string amiID)
        {
            var response = await _service.CreateInstanceAsync(amiID);
            return Ok(response);
        }
        [HttpDelete("{instanceId}")]
        public async Task<IActionResult> DeleteEC2Instance([FromRoute] string instanceId)
        {
            var response = await _service.TerminateInstanceAsync(instanceId);
            return Ok(response);
        }
     
        [HttpGet]
        [Route("/instances")]
        public async Task<IActionResult> GetAllInstances()
        {
            var response = await _service.GetAllInstancesAsync();
            return Ok(response);
        }

        [HttpPut]
        [Route("/instances/start/{id}")]
        public async Task<IActionResult> StartInstance(string id)
        {
            var response = await _service.StartInstanceAsync(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("/instances/stop/{id}")]
        public async Task<IActionResult> StopInstance(string id)
        {
            var response = await _service.StopInstanceAsync(id);
            return Ok(response);
        }
    }
}