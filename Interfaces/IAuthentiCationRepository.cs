using web_authentication.Dto;

namespace web_authentication.Interfaces
{
    public interface IAuthentiCationRepository
    {

        Task<string> Login(LoginModel model);
        Task<bool> Register(RegisterModel model);
       
    }
}
