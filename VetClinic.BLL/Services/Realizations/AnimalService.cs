using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class AnimalService : IAnimalService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AnimalService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Add(animal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<ICollection<Animal>> GetAllAsync()
        {
            return await _repositoryWrapper.AnimalRepository.GetAsync();
        }

        public async Task<Animal> GetAsync(int id)
        {
            return  (await _repositoryWrapper.AnimalRepository.GetAsync(x => x.Id == id)).FirstOrDefault();
        }

        public async Task UpdateAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Update(animal);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task RemoveAnimal(Animal animal)
        {
            _repositoryWrapper.AnimalRepository.Remove(animal);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
