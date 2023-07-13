using System.Data.Entity;
using web_authentication.Data;
using web_authentication.entities;
using web_authentication.Interfaces;

namespace web_authentication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            
            return await _dataContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateProduct(Product product)
        {
            _dataContext.Products.Update(product);
        }

        public async Task Create(Product product)
        {
            _dataContext.Add(product);
        }
        public async Task Delete(Product product)
        {
            _dataContext.Products.Remove(product);
        }
    }   
}
