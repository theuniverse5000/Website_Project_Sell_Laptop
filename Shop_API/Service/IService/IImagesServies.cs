using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Services.IServices
{
    public interface IImagesServies
    {
        //Task<string> UploadImages(IFormFile file, string c);
        Task<Guid> SaveImageAsync(Image image);
    }
}
