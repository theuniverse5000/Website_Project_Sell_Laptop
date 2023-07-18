using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("ProductDetail")]
    public class ProductDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        public float Price { get; set; }
        public float ImportPrice { get; set; }
        public string? Upgrade { get; set; }
        public string? Description { get; set; }
        [Range(0, 5, ErrorMessage = "Trạng thái phải từ 0 đến 5")]
        public int Status { get; set; }

        public Guid ProductId { get; set; }
        // [ForeignKey("Fk_ProductDetail_Color")]
        public Guid ColorId { get; set; }
        public Guid RamId { get; set; }
        public Guid CpuId { get; set; }
        public Guid HardDriveId { get; set; }
        public Guid ScreenId { get; set; }
        public Guid CardVGAId { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Ram? Ram { get; set; }
        public virtual Cpu? Cpu { get; set; }
        public virtual Screen? Screen { get; set; }
        public virtual CardVGA? CardVGA { get; set; }
        public virtual HardDrive? HardDrive { get; set; }
        public virtual Product? Product { get; set; }
        public ICollection<Image>? Imagess { get; set; }
        public ICollection<BillDetail>? BillDetails { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
        public ICollection<Serial>? Serials { get; set; }
    }
}
