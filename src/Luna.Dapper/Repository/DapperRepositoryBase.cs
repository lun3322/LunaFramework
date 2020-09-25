using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Luna.Repository;

namespace Luna.Dapper.Repository
{
    public abstract class DapperRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IDbConnection _connection;

        protected DapperRepositoryBase(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public IDbConnection GetConnection()
        {
            var connection = _connection;
            connection.Open();
            return connection;
        }

        public List<TEntity> GetAllList()
        {
            using (var dbConnection = GetConnection())
            {
                return dbConnection.Find<TEntity>().ToList();
            }
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public TEntity Get(TPrimaryKey id)
        {
            using (var dbConnection = GetConnection())
            {
                return dbConnection.Get(new TEntity {Id = id});
            }
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            using (var dbConnection = GetConnection())
            {
                dbConnection.Insert(entity);
                return entity.Id;
            }
        }

        public Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public void Update(TEntity entity)
        {
            using (var dbConnection = GetConnection())
            {
                dbConnection.Update(entity);
            }
        }

        public Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(TEntity entity)
        {
            using (var dbConnection = GetConnection())
            {
                dbConnection.Delete(entity);
            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

        public void Delete(TPrimaryKey id)
        {
            using (var dbConnection = GetConnection())
            {
                dbConnection.Delete(new TEntity {Id = id});
            }
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public int Count()
        {
            using (var dbConnection = GetConnection())
            {
                return dbConnection.Count<TEntity>();
            }
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }
    }
}