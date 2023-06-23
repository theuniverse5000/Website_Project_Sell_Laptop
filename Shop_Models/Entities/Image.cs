using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(150)]
        public string? LinkImage { get; set; }
        public int Status { get; set; }
        public Guid ProductDetailId { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
