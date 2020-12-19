using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IPositionService
    {
        public Task<Position> GetPositionAsync(int positionId);
        public Task<ICollection<Position>> GetPositionAsync();
        public Task<Position> AddPosition(Position position);
        public Task<bool> RemovePosition(int id);
        public Task<bool> UpdatePosition(Position position, int id);  

    }
}
