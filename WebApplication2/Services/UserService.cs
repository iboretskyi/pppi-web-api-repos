using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();

        public UserService(IPasswordService passwordService)
        {
            _users.AddRange(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Johnny",
                    LastName = "Silverhand",
                    Email = "johnny.silverhand@example.com",
                    DateOfBirth = new DateTime(1985, 1, 1),
                    PasswordHash = passwordService.HashPassword("password1"),
                    LastLogin = DateTime.UtcNow,
                    FailedLoginAttempts = 0,
                    IsBlocked = false
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PasswordHash = passwordService.HashPassword("password2"),
                    LastLogin = DateTime.UtcNow,
                    FailedLoginAttempts = 0,
                    IsBlocked = false
                },
                new User
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    PasswordHash = passwordService.HashPassword("password3"),
                    LastLogin = DateTime.UtcNow,
                    FailedLoginAttempts = 0,
                    IsBlocked = false
                }
            });
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public void UpdateFailedLoginAttempts(int userId, bool success)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                if (success)
                {
                    user.FailedLoginAttempts = 0;
                }
                else
                {
                    user.FailedLoginAttempts += 1;
                    if (user.FailedLoginAttempts >= 5)
                    {
                        user.IsBlocked = true;
                    }
                }
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public void BlockUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsBlocked = true;
            }
        }

        public void UnblockUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsBlocked = false;
                user.FailedLoginAttempts = 0;
            }
        }
    }
}
