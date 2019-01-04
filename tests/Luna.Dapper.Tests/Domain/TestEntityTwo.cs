using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Luna.Repository;

namespace Luna.Dapper.Tests.Domain
{
    [Table("TestEntityTwo")]
    public class TestEntityTwo : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
