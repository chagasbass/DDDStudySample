using System.Threading.Tasks;
using UserOperations.Core.Entities;

namespace UserOperations.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> VerifyUserAsync(string email);
        Task<User> GetUserByUserCodeAsync(string userCode);
   }
}
