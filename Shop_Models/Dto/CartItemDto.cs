namespace Shop_Models.Dto
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IdProductDetails { get; set; }
        public int Quantity { get; set; }
        public string? MaProductDetail { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdColor { get; set; }
        public Guid IdRam { get; set; }
        public Guid IdCpu { get; set; }
        public Guid IdHardDrive { get; set; }
        public Guid IdScreen { get; set; }
        public Guid IdCardVGA { get; set; }
        public string? MaRam { get; set; }
        public string? ThongSoRam { get; set; }
        public string? ThongSoHardDrive { get; set; }
        public string? MaHardDrive { get; set; }
        public string? MaCpu { get; set; }
        public string? TenCpu { get; set; }
        public string? NameColor { get; set; }
        public string? MaColor { get; set; }
        public string? NameProduct { get; set; }
        public string? NameManufacturer { get; set; }
        public string? MaManHinh { get; set; }
        public string? KichCoManHinh { get; set; }
        public string? TanSoManHinh { get; set; }
        public string? ChatLieuManHinh { get; set; }
        public string? MaCardVGA { get; set; }
        public string? TenCardVGA { get; set; }
        public string? ThongSoCardVGA { get; set; }
        public string? LinkImage { get; set; }
    }
}
