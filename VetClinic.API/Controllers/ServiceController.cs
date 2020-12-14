using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;

        public ServiceController(IMapper mapper, IServiceService serviceService)
        {
            _mapper = mapper;
            _serviceService = serviceService;
        }

        //GET api/service
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var services = await _serviceService.GetAllServices();
            var servicesDTO = _mapper.Map<ICollection<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }

        // GET api/service/3
        [HttpGet("{serviceId:min(1)}")]
        public async Task<ActionResult<ServiceDTO>> Get([FromRoute] int serviceId)
        {
            var service = await _serviceService.GetServiceById(serviceId);
            var serviceDTO = _mapper.Map<ServiceDTO>(service);
            return Ok(serviceDTO);
        }

        // POST api/service
        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> PostService([FromBody] ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            var insertedService  =  await _serviceService.Add(service);
            return Ok(insertedService.Id);
        }

        // PUT api/service
        [HttpPut]
        public async Task<IActionResult> UpdateService(ServiceDTO serviceDTO)
        {
            var service = _mapper.Map<Service>(serviceDTO);
            var updated = await _serviceService.Update(service);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/service/id
        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceService.Remove(id);
            if(!service)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
