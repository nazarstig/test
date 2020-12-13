using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

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
        public async Task<ICollection<ReadPositionDTO>> Get()
        {

            return _mapper.Map<ICollection<ReadPositionDTO>>(await _positionService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPositionDTO>> Get(int id)
        {
            var position = _mapper.Map<ReadPositionDTO>(await _positionService.GetAsync(id));
            if (position == null)
                return BadRequest();
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<CreatePositionDTO>> Post(CreatePositionDTO position)
        {
            await _positionService.Add(_mapper.Map<Position>(position));
            return Ok(position);
        }

        [HttpPut]
        public async Task<ActionResult<UpdatePositionDTO>> Put(UpdatePositionDTO position)
        {
            if (!(await _positionService.IsAnyAsync(position.Id)))
                return BadRequest();

            if (await _positionService.Update(_mapper.Map<Position>(position)))
            {
                return Ok(position);
            }

            return BadRequest();
        }

        [HttpDelete]


    }
}
