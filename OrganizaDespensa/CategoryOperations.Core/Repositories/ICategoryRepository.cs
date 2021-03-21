using CategoryOperations.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoryOperations.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync(string user);
        Task<bool> VerifyCategoryAsync(string name, string user);
        Task<Category> GetCategoryAsync(string id);
    }
}
