using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Luna.Repository;

namespace Luna.Dapper.Tests.Domain
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
