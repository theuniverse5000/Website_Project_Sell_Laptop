namespace Shop_Models.Dto
{
    public class BillDto
    {

        public string? InvoiceCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public object? BillDetail { get; set; }
        public int Count { get; set; } = 0;
    }
}
