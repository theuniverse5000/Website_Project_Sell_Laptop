using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface ISendMailService
    {
        Task SendMail(EmailDto mailContent);
    }
}
