using Microsoft.EntityFrameworkCore;
using web_authentication.Data;
using web_authentication.entities;
using web_authentication.Interfaces;

namespace web_authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }
        
        public async Task<User> GetUser(int id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(User user)
        {

        }
    }
}
