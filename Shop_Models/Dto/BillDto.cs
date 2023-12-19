namespace Shop_Models.Dto
{
    public class BillDto
    {

        public string? InvoiceCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? CodeVoucher { get; set; }
        public double? GiamGia { get; set; }
        public int Payment { get; set; }
        public bool IsPayment { get; set; }
        public Guid? UserId { get; set; }
        public int Status { get; set; }
        public object? BillDetail { get; set; }
        public int Count { get; set; } = 0;
    }
}
