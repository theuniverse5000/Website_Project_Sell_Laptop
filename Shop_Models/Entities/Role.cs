using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key] public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Descripion { get; set; }
        public int Status { get; set; }
        public virtual ICollection<User>? User { get; set; }

    }
}
