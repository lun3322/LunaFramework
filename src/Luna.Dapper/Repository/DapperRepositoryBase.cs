using Castle.Core;
using Dapper.FastCrud;
using Luna.Dapper.Uow;
using Luna.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Dapper.Repository
{
    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>, new()
    {
        public DapperRepositoryBase(IDbConnectionProvider dbConnectionProvider)
            : base(dbConnectionProvider)
        {
        }
    }

    [Interceptor(typeof(DapperUnitOfWorkInterceptor))]
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        public IDbConnection DbConnection => _dbConnectionProvider.GetDbConnection();

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperRepositoryBase(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public List<TEntity> GetAllList()
        {
            return DbConnection.Find<TEntity>().ToList();
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public TEntity Get(TPrimaryKey id)
        {
            return DbConnection.Get(new TEntity { Id = id });
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            DbConnection.Insert(entity);
            return entity.Id;
        }

        public Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public void Update(TEntity entity)
        {
            DbConnection.Update(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(TEntity entity)
        {
            DbConnection.Delete(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

        public void Delete(TPrimaryKey id)
        {
            DbConnection.Delete(new TEntity { Id = id });
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public int Count()
        {
            return DbConnection.Count<TEntity>();
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }
    }
}
