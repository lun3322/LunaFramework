using Castle.DynamicProxy;

namespace Luna.Dapper.Uow
{
    public class DapperUnitOfWorkInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //using (var dbConnection = _dbConnectionProvider.GetDbConnection())
            //using (var transaction = dbConnection.BeginTransaction())
            //{
            //    invocation.Proceed();
            //    transaction.Commit();
            //}
        }
    }
}
