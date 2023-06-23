using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Imei")]
    public class Imei
    {
        [Key]
        public Guid Id { get; set; }
        public int SoImei { get; set; }
        public int TrangThai { get; set; }
        public Guid BillDetailId { get; set; }
        public virtual BillDetail? BillDetail { get; set; }
    }
}
