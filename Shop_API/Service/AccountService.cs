using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Dto.Account;

namespace Shop_API.Service
{
    public class AccountService : IAccountService
    {
        public Task<LoginResponesDto> RenewToken(TokenDto tokenDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignUp(SignUpDto p)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponesDto> Validate(LoginRequestDto loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
