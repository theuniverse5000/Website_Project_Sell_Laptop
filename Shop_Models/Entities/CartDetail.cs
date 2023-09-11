using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("CartDetail")]
    public class CartDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }


    }
}
