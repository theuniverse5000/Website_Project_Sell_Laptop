using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("CPU")]
    public class Cpu
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? Ten { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public int TrangThai { get; set; }
        public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
