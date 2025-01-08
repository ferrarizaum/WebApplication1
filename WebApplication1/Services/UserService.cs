using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsers();
        Task<User> PostUser(User user);
        Task<User> UpdateUser(string login, string novoNome);
        Task<User> DeleteUser(string login);
        Task<bool> VerificaUser(string login, string userEmail, string guid);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private async Task<User> FindUserByLogin(string login)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> PostUser(User user)
        {
            try
            {
                if (await _dbContext.Users.AnyAsync(u => u.Login == user.Login))
                {
                    _logger.LogWarning("User with login {Login} already exists.", user.Login);
                    return null;
                }

                if (string.IsNullOrEmpty(user.ChaveVerificacao))
                {
                    user.ChaveVerificacao = Guid.NewGuid().ToString();
                }

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User {Login} created successfully.", user.Login);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user {Login}", user.Login);
                throw;
            }
        }

        public async Task<User> UpdateUser(string login, string novoNome)
        {
            try
            {
                var user = await FindUserByLogin(login);

                if (user == null)
                {
                    _logger.LogWarning("User with login {Login} not found.", login);
                    return null;
                }

                user.Nome = novoNome;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User {Login} updated successfully.", login);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user {Login}", login);
                throw;
            }
        }

        public async Task<User> DeleteUser(string login)
        {
            try
            {
                var user = await FindUserByLogin(login);

                if (user == null)
                {
                    _logger.LogWarning("User with login {Login} not found.", login);
                    return null;
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User {Login} deleted successfully.", login);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user {Login}", login);
                throw;
            }
        }
        // 3
        public async Task<bool> VerificaUser(string login, string userEmail, string guid)
        {
            try
            {
                var user = await FindUserByLogin(login);

                if (user == null)
                {
                    _logger.LogWarning("User with login {Login} not found.", login);
                    return false;
                }

                if (user.ChaveVerificacao == guid)
                {
                    // 3.A
                    user.IsVerificado = true;
                }

                user.Email = userEmail;
                user.ChaveVerificacao = guid;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User {Login} verification updated.", login);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying user {Login}", login);
                throw;
            }
        }
    }
}
