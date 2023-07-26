﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("SerialDaBan")]
    public class SerialDaBan
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? SerialNumber { get; set; }
        public int Status { get; set; }
        public Guid BillDetailId { get; set; }// Tạo khóa ngoại
        public virtual BillDetail? BillDetail { get; set; }
    }
}