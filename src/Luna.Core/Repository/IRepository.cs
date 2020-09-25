using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Luna.Dependency;

namespace Luna.Repository
{
    public interface IRepository<TEntity, TPrimaryKey> : IScopedDependency
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        IDbConnection GetConnection();
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