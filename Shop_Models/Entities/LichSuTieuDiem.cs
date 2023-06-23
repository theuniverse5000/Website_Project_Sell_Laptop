using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("LichSuTieuDiem")]
    public class LichSuTieuDiem
    {
        [Key] public Guid IdLichSuTieuDiem { get; set; }
        public double SoDiemDaDung { get; set; }
        public DateTime NgaySD { get; set; }
        public double SoDiemCong { get; set; }
        public int TrangThai { get; set; }
        public virtual QuyDoiDiem? QuyDoiDiem { get; set;}
        public virtual ViDiem? ViDiem { get; set; }
        public virtual Bill? Bill { get; set; }


    }
}
