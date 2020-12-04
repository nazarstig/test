using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.DAL.Repositories.Realizations
{
    class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationContext context) : base(context) { }
    }
}
