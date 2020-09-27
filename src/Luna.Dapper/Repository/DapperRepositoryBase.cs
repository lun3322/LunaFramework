using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
            using var dbConnection = GetConnection();
            return dbConnection.GetList<TEntity>().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            using var dbConnection = GetConnection();
            var listAsync = await dbConnection.GetListAsync<TEntity>();
            return listAsync.ToList();
        }

        public TEntity Get(TPrimaryKey id)
        {
            using var dbConnection = GetConnection();
            return dbConnection.Get<TEntity>(id);
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            using var dbConnection = GetConnection();
            return await dbConnection.GetAsync<TEntity>(id);
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            using var dbConnection = GetConnection();
            dbConnection.Insert(entity);
            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            using var dbConnection = GetConnection();
            await dbConnection.InsertAsync(entity);
            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            using var dbConnection = GetConnection();
            dbConnection.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var dbConnection = GetConnection();
            await dbConnection.UpdateAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            using var dbConnection = GetConnection();
            dbConnection.Delete(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using var dbConnection = GetConnection();
            await dbConnection.DeleteAsync(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            using var dbConnection = GetConnection();
            dbConnection.Delete(new TEntity {Id = id});
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            using var dbConnection = GetConnection();
            await dbConnection.DeleteAsync(new TEntity {Id = id});
        }

        public int Count()
        {
            using var dbConnection = GetConnection();
            return dbConnection.RecordCount<TEntity>();
        }

        public async Task<int> CountAsync()
        {
            using var dbConnection = GetConnection();
            return await dbConnection.RecordCountAsync<TEntity>();
        }
    }
}