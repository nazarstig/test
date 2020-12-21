using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.API.DTO.Service;
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
        public async Task<ActionResult<ICollection<ServiceDTO>>> Index()
        {
            var services = await _serviceService.GetAllServicesAsync();
            var servicesDTO = _mapper.Map<ICollection<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }

        // GET api/service/3
        [HttpGet("{id:min(1)}")]
        public async Task<ActionResult<ServiceDTO>> Show([FromRoute] int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            if(service == null)
            {
                return NotFound();
            }

            var serviceDTO = _mapper.Map<ServiceDTO>(service);
            return Ok(serviceDTO);
        }

        // POST api/service
        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> Create([FromBody] ServiceCreateDTO serviceCreateDTO)
        {
            var service = _mapper.Map<Service>(serviceCreateDTO);
            var insertedService  =  await _serviceService.AddAsync(service);
            var insertedServiceDTO = _mapper.Map<ServiceDTO>(insertedService);
            return CreatedAtAction("Get", new { id = insertedServiceDTO.Id }, insertedServiceDTO);
        }

        // PUT api/service/id
        [HttpPut("{id:min(1)}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]ServiceUpdateDTO serviceUpdateDTO)
        {
            var service = _mapper.Map<Service>(serviceUpdateDTO);
            var updated = await _serviceService.UpdateAsync(id, service);
            if (!updated)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // DELETE api/service/id
        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> Destroy(int id)
        {
            var service = await _serviceService.RemoveAsync(id);
            if(!service)
            {
                return NotFound();
            }
            
            return NoContent();
        }
       
    }
}
