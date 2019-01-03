using System.ComponentModel.DataAnnotations;
using Luna.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Luna.Dapper.Tests.Entities
{
    [Table("TestEntityThree")]
    public class TestEntityThree : IEntity<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
