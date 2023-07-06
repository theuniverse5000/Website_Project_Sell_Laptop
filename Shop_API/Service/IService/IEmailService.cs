using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface IEmailService
    {
        bool SendEmail(EmailDto request);
    }
}
