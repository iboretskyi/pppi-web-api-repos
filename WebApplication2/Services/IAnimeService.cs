using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime> GetByIdAsync(int id);
        Task<Anime> AddAsync(Anime anime);
        Task UpdateAsync(Anime anime);
        Task DeleteAsync(int id);
    }

}
