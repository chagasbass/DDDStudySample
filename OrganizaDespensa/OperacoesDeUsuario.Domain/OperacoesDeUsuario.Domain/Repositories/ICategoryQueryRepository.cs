using CategoryOperations.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoryOperations.Core.Repositories
{
    public interface ICategoryQueryRepository
    {
        Task<IEnumerable<CategoryListQuery>> GetCategoriesAsync(string usuario);
        Task<CategoryListQuery> GetCategoryAsync(string id);
    }
}
