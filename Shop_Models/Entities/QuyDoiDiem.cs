using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("QuyDoiDiem")]
    public class QuyDoiDiem
    {
        [Key] public Guid Id { get; set; }
        public double TienTichDiem { get; set; }
        public double TienTieuDiem { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<LichSuTieuDiem> LichSuTieuDiems { get; set; }
    }
}
