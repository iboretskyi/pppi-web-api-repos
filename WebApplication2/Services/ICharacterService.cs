using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character> GetByIdAsync(int id);
        Task<Character> AddAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(int id);
    }
}
