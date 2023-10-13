﻿using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto> AddCart(string username, string codeProductDetail);// Phương thức để người dùng tạo giỏ hàng
        Task<ResponseDto> CongQuantityCartDetail(Guid idCartDetail);// Cho khách hàng
        Task<ResponseDto> TruQuantityCartDetail(Guid idCartDetail); //Cho khách hàng
        Task<ResponseDto> GetAllCarts();// Cho admin quản lý
        Task<ResponseDto> GetCartByUsername(string username);// Cho admin quản lý
        Task<ResponseDto> GetCartJoinForUser(string username);// Hiển thị giỏ hàng cho người dùng
    }
}