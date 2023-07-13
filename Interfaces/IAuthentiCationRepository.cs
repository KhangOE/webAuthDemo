using web_authentication.Dto;
using web_authentication.entities;

namespace web_authentication.Interfaces
{
    public interface IAuthentiCationRepository
    {

        Task<string> Login(LoginModel model);
        Task<bool> Register(RegisterModel model);
        Task<AplicationUser> GetUserAuth();
       
    }
}
