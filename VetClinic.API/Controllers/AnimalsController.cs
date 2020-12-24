using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.Animal;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;
        public AnimalsController(IAnimalService animalService, IMapper mapper)
        {
            _animalService = animalService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateAnimalDto createAnimalDto)
        {
            await _animalService.CreateAnimal(_mapper.Map<Animal>(createAnimalDto));
            return Created(nameof(GetAsync), createAnimalDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var animals = await _animalService.GetAllAsync();
            var result = animals.Select(x => _mapper.Map<ReadAnimalDto>(x));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var animal = await _animalService.GetAsync(id);

            if(animal == null)
            {
                return NotFound();
            }

            var animalDto = _mapper.Map<ReadAnimalDto>(animal);
            return Ok(animalDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody]UpdateAnimalDto updateAnimalDto)
        {
            var animal = await _animalService.GetAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            //update fields
            animal.Name = updateAnimalDto.Name;
            animal.Age = updateAnimalDto.Age;
            animal.Photo = updateAnimalDto.Photo;
            animal.AnimalTypeId = updateAnimalDto.AnimalTypeId;

            await _animalService.UpdateAnimal(animal);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var animal = await _animalService.GetAsync(id);

            if (animal == null)
            {
                return NotFound();
            }
            await _animalService.RemoveAnimal(animal);
            return NoContent();
        }
    }
}
