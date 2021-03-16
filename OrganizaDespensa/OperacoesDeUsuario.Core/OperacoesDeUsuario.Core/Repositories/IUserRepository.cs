using OperacoesDeUsuario.Core.Entities;
using System.Threading.Tasks;

namespace OperacoesDeUsuario.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> VerifyUserAsync(string email);
        Task<User> GetUserByUserCodeAsync(string userCode);
   }
}
