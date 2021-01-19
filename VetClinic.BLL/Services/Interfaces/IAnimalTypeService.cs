using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IAnimalTypeService
    {
        Task CreateAnimalType(AnimalType animal);
        Task<ICollection<AnimalType>> GetAllAsync();
        Task<AnimalType> GetAsync(int id);
        Task RemoveAnimalType(AnimalType animal);
        Task UpdateAnimalType(AnimalType animal);
    }
}