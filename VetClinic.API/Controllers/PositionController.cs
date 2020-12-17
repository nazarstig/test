using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        public PositionController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;

        }
               
        [HttpGet]
        public async Task<ICollection<PositionDTO>> Get()
        {
            return _mapper.Map<ICollection<PositionDTO>>(await _positionService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDTO>> Get(int id)
        {
            var position = _mapper.Map<PositionDTO>(await _positionService.GetAsync(id));
            if (position == null)
                return NotFound();
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<PositionDTO>> Post(PositionDTO position)
        {
            await _positionService.Add(_mapper.Map<Position>(position));
            return Ok(position);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PositionDTO>> Put(PositionDTO position,int id)
        {            
            var successUpdate = await _positionService.Update((_mapper.Map<Position>(position)),id);
            if (successUpdate)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {   
            var successDelete = await _positionService.Remove(id);
            if (successDelete)
                return NotFound();
            return NoContent();
        }

    }
}
