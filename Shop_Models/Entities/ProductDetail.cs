using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("ProductDetail")]
    public class ProductDetail
    {
        [Key]
        public Guid Id { get; set; }
        public string Ma { get; set; }

        public decimal ImportPrice { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }

        public string Description { get; set; }


        //   public Guid IdProduct { get; set; }
        // [ForeignKey("Fk_ProductDetail_Color")]
        public Guid ColorId { get; set; }
        public Guid RamId { get; set; }
        public Guid CpuId { get; set; }
        public Guid HardDriveId { get; set; }
        public Guid ScreenId { get; set; }
        public Guid CardVGAId { get; set; }
        //     public virtual Product Product { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Ram? Ram { get; set; }
        public virtual Cpu? Cpu { get; set; }
        public virtual Screen? Screen { get; set; }
        public virtual CardVGA? CardVGA { get; set; }
        public virtual HardDrive? HardDrive { get; set; }
        //public ICollection<Image> Imagess { get; set; }
        //public ICollection<BillDetail> BillDetails { get; set; }
        //public ICollection<CartDetail> CartDetails { get; set; }
    }
}
