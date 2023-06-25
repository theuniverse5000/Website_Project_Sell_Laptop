﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid BillId { get; set; }
        public Guid ProductDetailId { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual ProductDetail? ProductDetails { get; set; }
        public virtual ICollection<Imei>? Imeis { get; set; }
    }
}