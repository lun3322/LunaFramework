using System.ComponentModel.DataAnnotations;

namespace Luna.Repository
{
    public interface IEntity<TPrimaryKey>
    {
        [Key]
        TPrimaryKey Id { get; set; }
    }
}