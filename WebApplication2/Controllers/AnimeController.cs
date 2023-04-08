using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        private static readonly string[] Genres = new[]
        {
            "Action", "Adventure", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Sci-Fi", "Thriller"
        };

        private static readonly List<Anime> animeList = new List<Anime>
        {
            new Anime { Id = 1, Title = "Dragon Ball Z", Genre = "Action", Episodes = 291, Rating = 8},
            new Anime { Id = 2, Title = "Sailor Moon", Genre = "Fantasy", Episodes = 200, Rating = 7},
            new Anime { Id = 3, Title = "Pokemon", Genre = "Adventure", Episodes = 276, Rating = 6},
            new Anime { Id = 4, Title = "Cowboy Bebop", Genre = "Sci-Fi", Episodes = 26, Rating = 9},
            new Anime { Id = 5, Title = "Yu Yu Hakusho", Genre = "Action", Episodes = 112, Rating = 8},
            new Anime { Id = 6, Title = "Rurouni Kenshin", Genre = "Adventure", Episodes = 94, Rating = 7},
            new Anime { Id = 7, Title = "Neon Genesis Evangelion", Genre = "Sci-Fi", Episodes = 26, Rating = 9},
            new Anime { Id = 8, Title = "Slam Dunk", Genre = "Sports", Episodes = 101, Rating = 8},
            new Anime { Id = 9, Title = "Saint Seiya", Genre = "Action", Episodes = 114, Rating = 7},
            new Anime { Id = 10, Title = "Berserk", Genre = "Fantasy", Episodes = 25, Rating = 9},
        };

        private readonly ILogger<AnimeController> _logger;

        public AnimeController(ILogger<AnimeController> logger)
        {
            _logger = logger;
        }

        // GET api/anime
        [HttpGet]
        public IActionResult GetAnimeList()
        {
            return Ok(animeList);
        }

        // GET api/anime/{id}
        [HttpGet("{id}", Name = "GetAnimeById")]
        public IActionResult GetAnimeById(int id)
        {
            var anime = animeList.FirstOrDefault(a => a.Id == id);
            if (anime == null)
            {
                return NotFound();
            }
            return Ok(anime);
        }


        // POST api/anime
        [HttpPost]
        public IActionResult AddAnime([FromBody] Anime anime)
        {
            // Add the new anime to the list
            animeList.Add(anime);

            // Return the newly created anime with the generated ID
            return CreatedAtRoute("GetAnimeById", new { id = anime.Id }, anime);
        }

    }
}
