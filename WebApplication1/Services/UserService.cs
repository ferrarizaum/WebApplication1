using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;

namespace WebApplication1.Services
{
   public interface IUserService
    {
        ICollection<User> GetUsers();
        User PostUser(User user);
        User UpdateUser(User user, string novoNome);
        User DeleteUser(string nome);
        void VerificaUser(string login, string userEmail, string guid);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<User> GetUsers()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }

        public User PostUser(User user)
        {
            if (string.IsNullOrEmpty(user.ChaveVerificacao))
            {
                user.ChaveVerificacao = Guid.NewGuid().ToString();
            }

            if (_dbContext.Users.Any(u => u.Login == user.Login))
            {
                return null;
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

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

        public void VerificaUser(string login, string userEmail, string guid)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                return;
            }

            if(user.ChaveVerificacao == guid)
            {
                user.IsVerificado = true;
            }

            user.Email = userEmail;
            user.ChaveVerificacao = guid;
            
            _dbContext.SaveChanges();
        }
    }
}
