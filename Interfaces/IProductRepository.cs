using web_authentication.entities;

namespace web_authentication.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProducts();
        Task<Product> GetProduct(int id);

        Task UpdateProduct(Product product);

        Task Create(Product product);

       
    }
}
