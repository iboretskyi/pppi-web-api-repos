using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void UpdateFailedLoginAttempts(int userId, bool success);
        void BlockUser(int userId);
        void UnblockUser(int userId);
        IEnumerable<User> GetAllUsers();
    }
}
