using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key] public Guid IdBillDetails { get; set; }
        public Guid IdBill { get; set; }
        public Guid IdProductDetails { get; set; }
        public int QuanTity { get; set; }
        public double Price { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual ProductDetail? ProductDetails { get; set; }
        public virtual IMei? IMeis { get; set; }
    }
}
