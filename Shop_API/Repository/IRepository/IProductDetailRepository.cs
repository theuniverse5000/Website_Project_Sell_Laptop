﻿using Shop_Models.Entities;
using Shop_Models.ViewModels;

namespace Shop_API.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        Task<bool> Create(ProductDetail obj);
        Task<bool> Update(ProductDetail obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<ProductDetail>> GetAll();
        Task<IEnumerable<ProductDetailView>> GetAllProductDetail();
        Task<IEnumerable<ProductDetailView>> GetAllProductDetailById(Guid id);
        Task<bool> UpdateSoLuong(Guid id, int soLuong);
    }
}