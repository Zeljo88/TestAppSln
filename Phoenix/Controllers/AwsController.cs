using Amazon.S3;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Phoenix.Services;
using Phoenix.Heaven.Interfaces;
using AutoMapper;

namespace Phoenix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwsController : ControllerBase
    {
        private readonly IAWSService _service;
        private readonly IMapper _mapper;

        public AwsController(IAWSService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost("{amiID}")]
        public async Task<IActionResult> CreateVM(string amiID)
        {
            var response = await _service.CreateVMAsync(amiID);
            return Ok(response);
        }
        [HttpDelete("{vmID}")]
        public async Task<IActionResult> DeleteVM(string instanceId)
        {
            var response = await _service.TerminateVMAsync(instanceId);
            return Ok(response);
        }
     
        [HttpGet]
        [Route("/vm")]
        public async Task<IActionResult> GetAllVMs()
        {
            var response = await _service.GetAllVMsAsync();
            return Ok(_mapper.Map<List<Models.VM>>(response));            
        }

        [HttpPut]
        [Route("/vm/start/{id}")]
        public async Task<IActionResult> StartVM(string id)
        {
            var response = await _service.StartVMAsync(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("/vm/stop/{id}")]
        public async Task<IActionResult> StopVM(string id)
        {
            var response = await _service.StopVMAsync(id);
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