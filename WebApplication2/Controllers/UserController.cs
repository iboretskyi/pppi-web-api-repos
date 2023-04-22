using AnimeWebAPI.Models;
using AnimeWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public ActionResult<User> GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost("{id}/block")]
        public IActionResult BlockUser(int id)
        {
            _userService.BlockUser(id);
            return Ok();
        }

        [HttpPost("{id}/unblock")]
        public IActionResult UnblockUser(int id)
        {
            _userService.UnblockUser(id);
            return Ok();
        }
    }
}
