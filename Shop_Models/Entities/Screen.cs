using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Screen")]
    public class Screen
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(50)]
        public string? KichCo { get; set; }
        [MaxLength(50)]
        public string? TanSo { get; set; }
        [MaxLength(50)]
        public string? ChatLieu { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
