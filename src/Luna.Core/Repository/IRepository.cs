using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luna.Dependency;

namespace Luna.Repository
{
    public interface IRepository : ITransientDependency
    {
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
    }

    public interface IRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : IEntity<TPrimaryKey>
    {
        List<TEntity> GetAllList();
        Task<List<TEntity>> GetAllListAsync();
        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);

        TPrimaryKey Insert(TEntity entity);
        Task<TPrimaryKey> InsertAsync(TEntity entity);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Update(TPrimaryKey id, Action<TEntity> updateAction);
        Task UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(TPrimaryKey id);
        Task DeleteAsync(TPrimaryKey id);

        int Count();
        Task<int> CountAsync();
    }
}