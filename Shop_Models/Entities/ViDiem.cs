using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("ViDiem")]
    public class ViDiem
    {
        [Key] public Guid IdViDiem { get; set; }
        public double TongDiem { get; set; }
        public double SoDiemDaDung { get; set; }
        public double SoDiemDaCong { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<LichSuTieuDiem> LichSuTieuDiems { get; set; }
        public virtual User? User { get; set; }

    }
}
