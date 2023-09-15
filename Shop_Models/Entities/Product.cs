using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }
        public int Status { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid ProductTypeId { get; set; }
        public string? PromotionType { get; set; } // thêm để làm giảm giá hàng loạt
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
