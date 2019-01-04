using System;
using System.Collections.Generic;
using System.Text;
using Luna.Repository;

namespace Luna.Dapper.Tests.Domain
{
    public interface ITestEntityRepository : IRepository<TestEntity, int>
    {
    }
}
