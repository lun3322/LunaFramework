using Castle.DynamicProxy;

namespace Luna.Dapper.Uow
{
    public class DapperUnitOfWorkInterceptor : IInterceptor
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperUnitOfWorkInterceptor(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            using (var dbConnection = _dbConnectionProvider.GetDbConnection())
            using (var transaction = dbConnection.BeginTransaction())
            {
                invocation.Proceed();
                transaction.Commit();
            }
        }
    }
}
