using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("LichSuTieuDiem")]
    public class LichSuTieuDiem
    {
        [Key]
        public Guid Id { get; set; }
        public double SoDiemDaDung { get; set; }
        public DateTime NgaySD { get; set; }
        public double SoDiemCong { get; set; }
        public int TrangThai { get; set; }
        public Guid QuyDoiDiemId { get; set; }
        public Guid ViDiemId { get; set; }
        public Guid BillId { get; set; }
        public virtual QuyDoiDiem? QuyDoiDiem { get; set; }
        public virtual ViDiem? ViDiem { get; set; }
        public virtual Bill? Bill { get; set; }


    }
}
