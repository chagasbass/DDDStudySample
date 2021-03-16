using CategoryOperations.Core.Entities;
using CategoryOperations.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using OrganizaDespensa.Infra.Core.DataContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoryOperations.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.InsertOneAsync(category);

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string user)
        {
            var categories = await _context.Categories.FindAsync(c => c.User.Equals(user));

            return categories.ToEnumerable();
        }

        public async Task<Category> GetCategoryAsync(string id)
        {
            var category = await _context.Categories.FindAsync(c => c.Id.Equals(ObjectId.Parse(id)));

            return category.FirstOrDefault();
        }

        public async Task<bool> VerifyCategoryAsync(string name, string user)
        {
            var category = await _context.Categories
               .FindAsync(c => c.Name.Equals(name) && c.User.Equals(user));

            return category.FirstOrDefault() != null;
        }
    }
}
