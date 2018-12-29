using Luna.Dependency;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Repository
{
    public interface IRepository : ITransientDependency
    {
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
    }

    public interface IRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();
        List<TEntity> GetAllList();
        Task<List<TEntity>> GetAllListAsync();
        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);

        TPrimaryKey Insert(TEntity entity);
        Task<TPrimaryKey> InsertAsync(TEntity entity);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(TPrimaryKey id);
        Task DeleteAsync(TPrimaryKey id);

        int Count();
        Task<int> CountAsync();
    }
}