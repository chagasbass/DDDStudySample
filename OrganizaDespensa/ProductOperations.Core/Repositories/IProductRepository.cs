using ProductOperations.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOperations.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<bool> VerifyProductoAsync(string name, string userId);
        Task<Product> GetProductAsync(string id);
        Task<IEnumerable<Product>> GetProductsAsync(string userId);
    }
}
