using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly List<Anime> _animes = new();

        public AnimeService()
        {
            _animes.AddRange(new List<Anime>
            {
                new Anime { Id = 1, Title = "Akira", Genre = "Sci-Fi", ReleaseDate = new DateTime(1988, 7, 16) },
                new Anime { Id = 2, Title = "Dragon Ball", Genre = "Action", ReleaseDate = new DateTime(1986, 2, 26) },
                new Anime { Id = 3, Title = "Sailor Moon", Genre = "Magical Girl", ReleaseDate = new DateTime(1992, 3, 7) },
                new Anime { Id = 4, Title = "Neon Genesis Evangelion", Genre = "Mecha", ReleaseDate = new DateTime(1995, 10, 4) },
                new Anime { Id = 5, Title = "Ghost in the Shell", Genre = "Sci-Fi", ReleaseDate = new DateTime(1995, 11, 18) },
                new Anime { Id = 6, Title = "Cowboy Bebop", Genre = "Sci-Fi", ReleaseDate = new DateTime(1998, 4, 3) },
                new Anime { Id = 7, Title = "Mobile Suit Gundam", Genre = "Mecha", ReleaseDate = new DateTime(1979, 4, 7) },
                new Anime { Id = 8, Title = "Ranma 1/2", Genre = "Comedy", ReleaseDate = new DateTime(1989, 4, 15) },
                new Anime { Id = 9, Title = "Slayers", Genre = "Fantasy", ReleaseDate = new DateTime(1995, 4, 7) },
                new Anime { Id = 10, Title = "Nadia: The Secret of Blue Water", Genre = "Adventure", ReleaseDate = new DateTime(1990, 4, 13) }
            });
        }

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return _animes;
        }

        public async Task<Anime> GetByIdAsync(int id)
        {
            return _animes.FirstOrDefault(a => a.Id == id);
        }

        public async Task<Anime> AddAsync(Anime anime)
        {
            _animes.Add(anime);
            return anime;
        }

        public async Task UpdateAsync(Anime anime)
        {
            var existingAnime = _animes.FirstOrDefault(a => a.Id == anime.Id);
            if (existingAnime != null)
            {
                existingAnime.Title = anime.Title;
                existingAnime.Genre = anime.Genre;
                existingAnime.ReleaseDate = anime.ReleaseDate;
                existingAnime.Characters = anime.Characters;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var anime = _animes.FirstOrDefault(a => a.Id == id);
            if (anime != null)
            {
                _animes.Remove(anime);
            }
        }
    }
}
