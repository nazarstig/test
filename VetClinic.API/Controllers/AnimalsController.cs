using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/animals")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;
        public AnimalsController(IAnimalService animalService, IMapper mapper)
        {
            _animalService = animalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var animals = await _animalService.GetAllAsync();
            var result = animals.Select(x => _mapper.Map<ReadAnimalDto>(x));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateAnimalDto createAnimalDto)
        {
            _animalService.CreateAnimal(_mapper.Map<Animal>(createAnimalDto));
            return Ok(createAnimalDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
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
        public async Task<IActionResult> Update(int id,[FromBody]UpdateAnimalDto updateAnimalDto)
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

            _animalService.UpdateAnimal(animal);

            return Ok(animal);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Destroy(int id)
        {
            var animal = await _animalService.GetAsync(id);

            if (animal == null)
            {
                return NotFound();
            }
            _animalService.RemoveAnimal(animal);
            return Ok(animal);
        }
    }
}
