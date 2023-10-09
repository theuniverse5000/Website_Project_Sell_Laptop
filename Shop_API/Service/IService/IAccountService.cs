using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface IAccountService
    {
        public Task<SignUpRespone> SignUp(SignUpDto p);
        public Task<LoginResponesDto> RenewToken(TokenDto tokenDTO);
        public Task<LoginResponesDto> Validate(LoginRequestDto loginRequest);
    }
}