using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public Guid UserId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartDetail>? CartDetails { get; set; }
    }
}
