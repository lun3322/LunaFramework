namespace Luna.Repository
{
    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IEntity
    {

    }
}