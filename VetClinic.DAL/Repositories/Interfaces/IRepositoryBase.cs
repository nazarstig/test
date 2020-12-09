using System.Linq;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAsync();
        Task<TEntity> GetAsync(int id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}