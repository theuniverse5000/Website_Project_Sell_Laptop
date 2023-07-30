using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface ICartService
    {
        Task<ReponseDto> AddCart(string username, string codeProductDetail);// Phương thức để người dùng tạo giỏ hàng
        Task<ReponseDto> CongQuantityCartDetail(Guid idCartDetail);// Cho khách hàng
        Task<ReponseDto> TruQuantityCartDetail(Guid idCartDetail); //Cho khách hàng
        Task<ReponseDto> GetAllCarts();// Cho admin quản lý
        Task<ReponseDto> GetCartByUsername(string username);// Cho admin quản lý
        Task<ReponseDto> GetCartJoinForUser(string username);// Hiển thị giỏ hàng cho người dùng
    }
}
