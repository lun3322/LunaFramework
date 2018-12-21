using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;

namespace Luna.Repository.Dapper
{
    public class DapperRepository<TEntity> : DapperRepository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public DapperRepository(IDbConnectionProvider dbConnectionProvider)
            : base(dbConnectionProvider)
        {
        }
    }

    public class DapperRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        private DbConnection DbConnection
        {
            get
            {
                var dbConnection = _dbConnectionProvider.GetDbConnection();
                dbConnection.Open();
                return dbConnection as DbConnection;
            }
        }

        public List<TEntity> GetAllList()
        {
            return DbConnection.GetList<TEntity>().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await Task.FromResult(GetAllList());
        }

        public TEntity Get(TPrimaryKey id)
        {
            return DbConnection.Get<TEntity>(id);
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await Task.FromResult(Get(id));
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            var newid = DbConnection.Insert(entity);
            return newid;
        }

        public async Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Insert(entity));
        }

        public void Update(TEntity entity)
        {
            DbConnection.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            await Task.FromResult(0);
        }

        public void Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            DbConnection.Update(entity);
        }

        public async Task UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction.Invoke(entity);
            DbConnection.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DbConnection.Delete(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            await Task.FromResult(0);
        }

        public void Delete(TPrimaryKey id)
        {
            var field = Predicates.Field<TEntity>(m => m.Id, Operator.Eq, id);
            DbConnection.Delete<TEntity>(field);
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            await Task.FromResult(0);
        }

        public int Count()
        {
            return DbConnection.Count<TEntity>(null);
        }

        public async Task<int> CountAsync()
        {
            return await Task.FromResult(Count());
        }
    }
}