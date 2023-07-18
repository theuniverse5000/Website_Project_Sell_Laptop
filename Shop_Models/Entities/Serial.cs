using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Serial")]
    public class Serial
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? SerialNumber { get; set; }
        public int Status { get; set; }
        public Guid ProductDetailId { get; set; }// Tạo khóa ngoại
        public virtual ProductDetail? ProductDetail { get; set; }//
    }
}
