using MongoDB.Driver;
using OperacoesDeUsuario.Core.Entities;
using OperacoesDeUsuario.Core.Repositories;
using OrganizaDespensa.Infra.Core.DataContexts;
using System.Threading.Tasks;

namespace UserOperations.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await  _context.Users.InsertOneAsync(user);

            return user;
        }

        public async  Task<User> GetUserByUserCodeAsync(string userCode)
        {
            var users = await _context.Users
                 .FindAsync(x => x.UserCode.Equals(userCode));

            return users.FirstOrDefault();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _context.Users.ReplaceOneAsync(x=> x.Id == user.Id, user);
            return user;
        }

        public async Task<bool> VerifyUserAsync(string email)
        {
            var user = await _context.Users
                .FindAsync(x => x.Email.Endereco.Equals(email));

            return user.FirstOrDefault() != null;
            
        }
    }
}
