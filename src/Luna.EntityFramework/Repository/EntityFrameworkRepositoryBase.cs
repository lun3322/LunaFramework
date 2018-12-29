using Luna.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Luna.EntityFramework.Repository
{
    public class EntityFrameworkRepositoryBase<TDbContext, TEntity> : EntityFrameworkRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {

    }

    public class EntityFrameworkRepositoryBase<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        // todo 获取到当前的 dbcontent
        public TDbContext DbContext { get; set; }

        public DbSet<TEntity> Table => DbContext.Set<TEntity>();

        public IDbConnection DbConnection => DbContext.Database.GetDbConnection();

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public List<TEntity> GetAllList()
        {
            return Table.ToList();
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return Table.ToListAsync();
        }

        public TEntity Get(TPrimaryKey id)
        {
            var lambda = CreateCompareIdExpression(id);

            return Table.FirstOrDefault(lambda);
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var expression = CreateCompareIdExpression(id);
            return Table.FirstOrDefaultAsync(expression);
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            return Table.Add(entity).Entity.Id;
        }

        public Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(0);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = Get(id);
            Table.Remove(entity);
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            var entity = Get(id);
            Table.Remove(entity);
            return Task.FromResult(0);
        }

        public int Count()
        {
            return Table.Count();
        }

        public Task<int> CountAsync()
        {
            return Table.CountAsync();
        }

        private static Expression<Func<TEntity, bool>> CreateCompareIdExpression(TPrimaryKey id)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var expression = Expression.Equal(Expression.PropertyOrField(parameter, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey)));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
            return lambda;
        }
    }
}
