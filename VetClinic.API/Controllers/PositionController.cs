using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        public PositionController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;

        }
        
        [HttpGet]
        public async Task<ICollection<ReadPositionDTO>> Get()
        {
            
            var positions =  _mapper.Map<ICollection<ReadPositionDTO>>(await _repositoryWrapper.PositionRepository.GetAsync());
            return positions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPositionDTO>> Get(int id)
        {
            var position = _mapper.Map<ReadPositionDTO>(await _repositoryWrapper.PositionRepository.GetFirstOrDefaultAsync(p=> p.Id==id));
            if (position == null)
                return BadRequest();
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<CreatePositionDTO>> Post(CreatePositionDTO position)
        {
             _repositoryWrapper.PositionRepository.Add(_mapper.Map<Position>(position));
            await _repositoryWrapper.SaveAsync();
            return Ok(position);
        }

        [HttpPut]
        public async Task<ActionResult<UpdatePositionDTO>> Put(UpdatePositionDTO position)
        {
            Position pos =await _repositoryWrapper.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == position.Id);
            pos.PositionName = position.PositionName;
            pos.Salary = position.Salary;
            _repositoryWrapper.PositionRepository.Update(pos);
            await _repositoryWrapper.SaveAsync();
            return Ok(position);
        }




    }
}
