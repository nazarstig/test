using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.AnimalType;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalTypesController : ControllerBase
    {
        private readonly IAnimalTypeService _animalTypeService;
        private readonly IMapper _mapper;
        public AnimalTypesController(IAnimalTypeService animalTypeService, IMapper mapper)
        {
            _animalTypeService = animalTypeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AnimalTypeDto createAnimalTypeDto)
        {
            await _animalTypeService.CreateAnimalType(_mapper.Map<AnimalType>(createAnimalTypeDto));
            return Created(nameof(GetAsync), createAnimalTypeDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var animals = await _animalTypeService.GetAllAsync();
            var result = animals.Select(x => _mapper.Map<ReadAnimalTypeDto>(x));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var animal = await _animalTypeService.GetAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            var animalTypeDto = _mapper.Map<ReadAnimalTypeDto>(animal);
            return Ok(animalTypeDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] AnimalTypeDto updateAnimalTypeDto)
        {
            var animalType = await _animalTypeService.GetAsync(id);

            if (animalType == null)
            {
                return NotFound();
            }

            //update fields
            animalType.AnimalTypeName = updateAnimalTypeDto.AnimalTypeName;

            await _animalTypeService.UpdateAnimalType(animalType);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var animalType = await _animalTypeService.GetAsync(id);

            if (animalType == null)
            {
                return NotFound();
            }
            await _animalTypeService.RemoveAnimalType(animalType);
            return NoContent();
        }
    }
}
