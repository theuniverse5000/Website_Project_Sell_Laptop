using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("GiamGia")]
    public class GiamGia
    {
        [Key] public  Guid ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal MucGiamGiaPhanTram { get; set; }
        public double MucGiamGiaTienMat { get; set; }
        public string TrangThai { get; set; }
        public string LoaiGiamGia { get; set; }
        public virtual ICollection<SanPhamGiamGia> SanPhamGiamGias { get; set; }
    }
}
