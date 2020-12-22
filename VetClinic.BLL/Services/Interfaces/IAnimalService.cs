using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IAnimalService
    {
        void CreateAnimal(Animal animal);
        Task<ICollection<Animal>> GetAllAsync();
        void UpdateAnimal(Animal animal);
        Task<Animal> GetAsync(int id);
        void RemoveAnimal(Animal animal);
    }
}
