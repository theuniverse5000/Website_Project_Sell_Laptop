using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Ram")]
    public class Ram
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? ThongSo { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
