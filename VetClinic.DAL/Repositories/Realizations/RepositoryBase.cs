using System.Linq;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.DAL.Repositories.Realizations
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected ApplicationContext Context { get; }

        protected RepositoryBase(ApplicationContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> GetAsync()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}