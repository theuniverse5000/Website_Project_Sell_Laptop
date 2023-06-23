using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key] public Guid IdVoucher { get; set; }
        public string MaVoucher { get; set; }
        public string TenVoucher { get; set; }
        public DateTime StarDay { get; set; }
        public DateTime EndDay { get; set; }
        public double GiaTri { get; set; }
        public int SoLuong { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
