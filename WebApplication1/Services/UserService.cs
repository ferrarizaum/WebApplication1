using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Services
{
   public interface IUserService
    {
        Task<ICollection<User>> GetUsers();
        Task<User> PostUser(User user);
        User UpdateUser(User user, string novoNome);
        User DeleteUser(string nome);
        Task<bool> VerificaUser(string login, string userEmail, string guid);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> PostUser(User user)
        {
            // 2.A
            if (_dbContext.Users.Any(u => u.Login == user.Login))
            {
                return null;
            }

            if (string.IsNullOrEmpty(user.ChaveVerificacao))
            {
                // 2.C
                user.ChaveVerificacao = Guid.NewGuid().ToString();
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public User UpdateUser(User user, string novoNome)
        {
            throw new NotImplementedException();
        }
        public User DeleteUser(string nome)
        {
            throw new NotImplementedException();
        }
        // 3
        public async Task<bool> VerificaUser(string login, string userEmail, string guid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
            {
                return false;
            }

            if(user.ChaveVerificacao == guid)
            {
                // 3.A
                user.IsVerificado = true;
            }

            user.Email = userEmail;
            user.ChaveVerificacao = guid;
            
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
