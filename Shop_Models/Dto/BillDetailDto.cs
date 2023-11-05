namespace Shop_Models.Dto
{
    public class BillDetailDto
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string? CodeProductDetail { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public string? ThongSoRam { get; set; }
        public string? ThongSoHardDrive { get; set; }
        public string? TenCpu { get; set; }
        public string? NameColor { get; set; }
        public string? NameProduct { get; set; }
        public string? NameManufacturer { get; set; }
        public string? KichCoManHinh { get; set; }
        public string? TanSoManHinh { get; set; }
        public string? ChatLieuManHinh { get; set; }
        public string? TenCardVGA { get; set; }
        public string? ThongSoCardVGA { get; set; }
        public string? SerialNumber { get; set; }
    }
}
