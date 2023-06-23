using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Bill")]
    public class Bill
    {
        [Key] public Guid Id { get; set; }
        public string? Ma { get; }
        public DateTime CreateDate { get; }

        [MaxLength(20)]
        public string? SDTKhachHang { get; set; }
        [MaxLength(150)]
        public string? HoTenKhachHang { get; set; }
        [MaxLength(150)]
        public string? DiaChiKhachHang { get; set; }
        public int Status { get; set; }
        public Guid? VoucherId { get; set; }
        public Guid? UserId { get; }
        public virtual Voucher? Vouchers { get; set; }
        public virtual User? Users { get; set; }
        public virtual ICollection<LichSuTieuDiem>? LichSuTieuDiems { get; set; }
        public virtual ICollection<BillDetail>? BillDetails { get; set; }


    }
}
