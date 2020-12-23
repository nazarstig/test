using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IAnimalService
    {
        Task CreateAnimal(Animal animal);
        Task<ICollection<Animal>> GetAllAsync();
        Task UpdateAnimal(Animal animal);
        Task<Animal> GetAsync(int id);
        Task RemoveAnimal(Animal animal);
    }
}
