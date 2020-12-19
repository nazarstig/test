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
        public async Task<ActionResult<ICollection<PositionDTO>>> Index()
        {
            var positionsDTO = _mapper.Map<ICollection<PositionDTO>>(await _positionService.GetPositionAsync());
            return Ok(positionsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDTO>> Show(int id)
        {
            var position = await _positionService.GetPositionAsync(id);
            var positionDTO = _mapper.Map<PositionDTO>(position);
            if (positionDTO == null)
                return NotFound();
            return Ok(positionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PositionDTO>> Create(PositionDTO position)
        {
            var createdPosition =await _positionService.AddPosition(_mapper.Map<Position>(position));
            var positionDto = _mapper.Map<PositionDTO>(createdPosition);

            return Ok(positionDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PositionDTO>> Update(PositionDTO position, int id)
        {
            var successUpdate = await _positionService.UpdatePosition((_mapper.Map<Position>(position)), id);
            if (successUpdate)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Destroy(int id)
        {
            var successDelete = await _positionService.RemovePosition(id);
            if (successDelete)
                return NoContent();

            return NotFound();
        }

    }
}
