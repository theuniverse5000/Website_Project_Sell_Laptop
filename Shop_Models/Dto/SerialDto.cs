using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class SerialDto
    {
        public Guid Id { get; set; }
        public string? SerialNumber { get; set; }
        public int Status { get; set; }
        public Guid? ProductDetailId { get; set; }// Tạo khóa ngoại
        public string? ProductDetailCode { get; set; }
        public Guid? BillDetailId { get; set; }// Tạo khóa ngoại
        public string? BillDetailCode { get; set; }
        
    }
}
