using web_authentication.entities;

namespace web_authentication.Interfaces
{
    public interface IUserRepository
    {

        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int id);
        Task Create(User user);

    }
}
