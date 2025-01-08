using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<string> Login(string login);
        string Generate(User user);
    }
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // 7
        public async Task<string> Login(string login)
        {
            var userToInsertToken = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (userToInsertToken == null)
            {
                return null;
            }

            string token = Generate(userToInsertToken);

            if(token == null)
            {
                return null;
            }

            userToInsertToken.LastToken = token;

            await _dbContext.SaveChangesAsync();

            return token;
        }
        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(
                new Claim(ClaimTypes.Name, user.Nome));

            return ci;
        }

    }
}
