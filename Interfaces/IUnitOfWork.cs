namespace web_authentication.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }

        IAuthentiCationRepository AuthentiCationRepository { get; }
        Task SavechangesAsync();
    }
}
