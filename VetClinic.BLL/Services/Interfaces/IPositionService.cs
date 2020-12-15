using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IPositionService
    {
        public Task<Position> GetAsync(int positionId);
        public Task<ICollection<Position>> GetAsync();
        public Task<Position> Add(Position position);
        public Task<bool> Remove(int positionId);
        public Task<bool> Update(Position position);  

    }
}
