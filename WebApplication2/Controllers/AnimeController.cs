using AnimeWebAPI.Models;
using AnimeWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        // GET: api/anime
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var animes = await _animeService.GetAllAsync();
            return Ok(animes);
        }

        // GET: api/anime/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var anime = await _animeService.GetByIdAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            return Ok(anime);
        }

        // POST: api/anime
        [HttpPost]
        public async Task<IActionResult> AddAsync(Anime anime)
        {
            var createdAnime = await _animeService.AddAsync(anime);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdAnime.Id }, createdAnime);
        }

        // PUT: api/anime/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest();
            }

            await _animeService.UpdateAsync(anime);
            return NoContent();
        }

        // DELETE: api/anime/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _animeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
