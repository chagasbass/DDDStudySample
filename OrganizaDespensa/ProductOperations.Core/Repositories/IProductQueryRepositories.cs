using ProductOperations.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductOperations.Core.Repositories
{
    public interface IProductQueryRepositories
    {
        Task<IEnumerable<ProductListQuery>> GetProductsAsync(string user);
    }
}
