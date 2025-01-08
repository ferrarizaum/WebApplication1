using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsers();

            if (users == null || users.Count == 0)
            {
                return NoContent();  
            }

            return Ok(users);  
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");  
            }

            var newUser = await _userService.PostUser(user);

            if (newUser == null)
            {
                return Conflict("User with the same login already exists.");  
            }

            return CreatedAtAction(nameof(Get), new { login = newUser.Login }, newUser); 
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] string login, [FromQuery] string novoNome)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(novoNome))
            {
                return BadRequest("Login and novoNome cannot be null or empty.");  
            }

            var updatedUser = await _userService.UpdateUser(login, novoNome);

            if (updatedUser == null)
            {
                return NotFound("User not found.");  
            }

            return Ok(updatedUser);  
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return BadRequest("Login cannot be null or empty.");  
            }

            var removedUser = await _userService.DeleteUser(login);

            if (removedUser == null)
            {
                return NotFound("User not found.");  
            }

            return Ok(removedUser);  
        }

        [HttpPost]
        [Route("verificaEmail")]
        public async Task<IActionResult> Verifica([FromQuery] string login, [FromQuery] string userEmail, [FromQuery] string guid)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(guid))
            {
                return BadRequest("Login, email, and guid must be provided."); 
            }

            var isVerified = await _userService.VerificaUser(login, userEmail, guid);

            if (!isVerified)
            {
                return BadRequest("Invalid verification details.");  
            }

            return Ok("User verified successfully.");  
        }
    }
}
