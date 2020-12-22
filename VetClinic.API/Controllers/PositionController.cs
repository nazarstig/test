using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;  

        public PositionController(IPositionService positionService, IMapper mapper )
        {
            _positionService = positionService;
            _mapper = mapper;            

        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PositionDto>>> Index()
        {
            var positionsDTO = _mapper.Map<ICollection<PositionDto>>(await _positionService.GetPositionAsync());
            return Ok(positionsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> Show(int id)
        {
            var position = await _positionService.GetPositionAsync(id);
            var positionDTO = _mapper.Map<PositionDto>(position);
            if (positionDTO == null)
                return NotFound();
            return Ok(positionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PositionDto>> Create(PositionDto position)
        {
            var createdPosition =await _positionService.AddPositionAsync(_mapper.Map<Position>(position));
            var positionDto = _mapper.Map<PositionDto>(createdPosition);

            return CreatedAtAction(nameof(Show), new { id = positionDto.Id }, positionDto);
        }
    

        [HttpPut("{id}")]
        public async Task<ActionResult<PositionDto>> Update(PositionDto position, int id)
        {
            var successUpdate = await _positionService.UpdatePositionAsync((_mapper.Map<Position>(position)), id);
            if (successUpdate)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Destroy(int id)
        {
            var successDelete = await _positionService.RemovePositionAsync(id);
            if (successDelete)
                return NoContent();

            return NotFound();
        }

    }
}
