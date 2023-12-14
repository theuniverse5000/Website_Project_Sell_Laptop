using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class TichDiemDto
    {
        public double TienTieuDiem { get; set; }
        public double TienTichDiem { get; set; }
        public double SoDiemDaDungTrongHoaDon { get; set; }
        public double SoDiemCongTrongHoaDon { get; set; }
        public DateTime NgaySD { get; set; }
        public double TongDiemTrongViDiem { get; set; }
        public double SoDiemDaCongTrongVi { get; set; }
        public double SoDiemDaDungTrongVi { get; set; }
        public int TrangThaiViDiem { get; set; }

    }
}
