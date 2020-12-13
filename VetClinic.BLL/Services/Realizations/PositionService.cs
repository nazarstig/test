using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class PositionService : IPositionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public PositionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<Position> Add(Position position)
        {
            _repositoryWrapper.PositionRepository.Add(position);
            await _repositoryWrapper.SaveAsync();
            return position;
        }

        public async Task<Position> GetAsync(int positionId)
        {
            return await _repositoryWrapper.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == positionId);
        }

        public async Task<ICollection<Position>> GetAsync()
        {
            return await _repositoryWrapper.PositionRepository.GetAsync();

        }

        public async Task<bool> IsAnyAsync(int positionId)
        {
            return await _repositoryWrapper.PositionRepository.IsAnyAsync(p => p.Id == positionId);
        }

        public Task<bool> Remove(int positionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Position position)
        {
            _repositoryWrapper.PositionRepository.Update(position);
            await _repositoryWrapper.SaveAsync();
            return true;
        }
    }
}
