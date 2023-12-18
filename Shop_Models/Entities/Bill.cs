using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? InvoiceCode { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(150)]
        public string? FullName { get; set; }
        [MaxLength(150)]
        public string? Address { get; set; }
        public int Payment { get; set; }
        public bool IsPayment { get; set; }
        public float? Discount { get; set; }
        public int Status { get; set; }
        public Guid? VoucherId { get; set; }
        public Guid? UserId { get; set; }
        public virtual Voucher? Vouchers { get; set; }
        public virtual User? Users { get; set; }
        public virtual ICollection<BillDetail>? BillDetails { get; set; }


    }
}
