using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key] public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? MaVoucher { get; set; }
        [MaxLength(150)]
        public string? TenVoucher { get; set; }
        public DateTime StarDay { get; set; }
        public DateTime EndDay { get; set; }
        public double GiaTri { get; set; }
        public int SoLuong { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
