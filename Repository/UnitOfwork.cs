using web_authentication.Data;
using web_authentication.Interfaces;

namespace web_authentication.Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private DataContext _dataContext;
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
        private IAuthentiCationRepository _authentiCationRepository;
        public UnitOfwork(DataContext dataContext,IAuthentiCationRepository authentiCationRepository,IProductRepository productRepository,IUserRepository userRepository) {
        
            _dataContext = dataContext;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _authentiCationRepository = authentiCationRepository;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new ProductRepository(_dataContext);

                }
                return _productRepository;
            }
        }

        public IAuthentiCationRepository AuthentiCationRepository
        {
            get
            {
             /*   if (this._authentiCationRepository == null)
                {
                    this._authentiCationRepository = new AuthenticationRepository(_dataContext);

                }*/
                return _authentiCationRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_dataContext);

                }
                return _userRepository;
            }
        }

        public async Task SavechangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
