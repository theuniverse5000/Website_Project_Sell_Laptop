using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class SanPhamGiamGiaDto
    {
        public Guid Id { get; set; }
        public double DonGia { get; set; }
        public double SoTienConLai { get; set; }
        public int TrangThai { get; set; }
        public Guid ProductDetailId { get; set; }
        public Guid GiamGiaId { get; set; }
        public string? ProductDetailCode { get; set; }
        public string? ProductDetailName { get; set; }
        public string? GiamGiaCode { get; set; }
        public string? TenSanPham { get; set; }
        public string? LinkImage { get; set; }
        public decimal? GiamGiaPhanTram { get; set; }
        public virtual GiamGia? GiamGia { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
