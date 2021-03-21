using OrganizaDespensa.Infra.Core.DataContexts;
using ProductOperations.Core.Entities;
using ProductOperations.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProductOperations.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);

            return product;
        }

        public Task DeleteProductAsync(Product product)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            var product = await _context.Products.FindAsync(x => x.Id.Equals(ObjectId.Parse(id)));

            return await product.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string userId)
        {
            var products = await _context.Products
               .FindAsync(x => x.UserId.Equals(userId));

            return products.ToEnumerable();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return product;
        }

        public async Task<bool> VerifyProductoAsync(string name, string userId)
        {
            var product = await _context.Products
               .FindAsync(x => x.ProductData.Name.Equals(name.Trim()) && x.UserId.Equals(userId));

            var requestedProduct = await product.FirstOrDefaultAsync();

            if (requestedProduct == null)
                return false;

            return true;
        }
    }
}
