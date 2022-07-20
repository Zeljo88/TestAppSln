using Amazon.S3;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Heaven.Interfaces;
using TestApp.Services;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwsController : ControllerBase
    {
        private readonly IVMService _service;         

        public AwsController(IVMService service)
        {
            _service = service;
        }
        [HttpPost("{amiID}")]
        public async Task<IActionResult> CreateEC2Instance([FromRoute] string amiID)
        {
            var response = await _service.CreateVMAsync(amiID);
            return Ok(response);
        }
        [HttpDelete("{vmID}")]
        public async Task<IActionResult> DeleteEC2Instance([FromRoute] string instanceId)
        {
            var response = await _service.TerminateInstanceAsync(instanceId);
            return Ok(response);
        }
     
        [HttpGet]
        [Route("/vm")]
        public async Task<IActionResult> GetAllInstances()
        {
            var response = await _service.GetAllInstancesAsync();
            return Ok(response);
        }

        [HttpPut]
        [Route("/vm/start/{id}")]
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


        [HttpGet]
        [Route("/stack/resources")]
        public async Task<IActionResult> GetAllStackResources()
        {
            var response = await _service.ListResources();
            return Ok(response);
        }

        [HttpPost]
        [Route("/stack")]
        public async Task<IActionResult> CreatStack()
        {
            var response = await _service.CreateStackAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("/s3/buckets/templates")]
        public async Task<IActionResult> GetAllTemplates()
        {
            var response = await _service.ListingObjectsAsync();
            return Ok(response);
        }
    }
}