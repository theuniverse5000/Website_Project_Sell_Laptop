using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
