using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IPositionService
    {
        public Task<Position> GetPositionAsync(int positionId);
        public Task<ICollection<Position>> GetPositionAsync();
        public Task<Position> AddPositionAsync(Position position);
        public Task<bool> RemovePositionAsync(int id);
        public Task<bool> UpdatePositionAsync(Position position, int id);  

    }
}
