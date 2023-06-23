using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("IMei")]
    public class IMei
    {
        [Key] public Guid ID { get; set; }
        public string SoIMei { get; set; }
        public int TrangThai { get; set; }
        public Guid? IdBillDetail { get; set; }
        public virtual BillDetail? BillDetail { get; set; }
    }
}
