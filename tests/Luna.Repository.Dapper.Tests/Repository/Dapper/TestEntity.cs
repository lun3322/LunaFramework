namespace Luna.Repository.Dapper.Tests.Repository.Dapper
{
    public class TestEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestEntity1 : IEntity<long>
    {
        public long Id { get; set; }
    }
}