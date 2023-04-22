using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimeWebAPI.Models;
using AnimeWebAPI.Services;
using AnimeWebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnimeWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IPasswordService passwordService, IConfiguration config)
        {
            _userService = userService;
            _passwordService = passwordService;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Невірні вхідні дані.");
            }

            var user = _userService.GetUserByEmail(loginRequest.Email);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.PasswordHash);
            if (user == null || user.IsBlocked || !_passwordService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                _userService.UpdateFailedLoginAttempts(user.Id, false);
                return Unauthorized("Неправильний логін або пароль.");
            }

            _userService.UpdateFailedLoginAttempts(user.Id, true);

            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Невірні вхідні дані.");
            }

            var existingUser = _userService.GetUserByEmail(registerRequest.Email);
            if (existingUser != null)
            {
                return BadRequest("Користувач з такою електронною адресою вже існує.");
            }

            var newUser = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                DateOfBirth = registerRequest.DateOfBirth,
                PasswordHash = _passwordService.HashPassword(registerRequest.Password),
                LastLogin = null,
                FailedLoginAttempts = 0,
                IsBlocked = false
            };

            return Ok("Успішна реєстрація");
        }
    }
}
