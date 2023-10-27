using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class PointWalletDto
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public double TongDiem { get; set; }
        public double SoDiemDaDung { get; set; }
        public double SoDiemDaCong { get; set; }
        public int TrangThai { get; set; }
    }
}
