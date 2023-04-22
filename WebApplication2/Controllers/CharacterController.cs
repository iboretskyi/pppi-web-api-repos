using AnimeWebAPI.Models;
using AnimeWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET: api/character
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var characters = await _characterService.GetAllAsync();
            return Ok(characters);
        }

        // GET: api/character/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var character = await _characterService.GetByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        // POST: api/character
        [HttpPost]
        public async Task<IActionResult> AddAsync(Character character)
        {
            var createdCharacter = await _characterService.AddAsync(character);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCharacter.Id }, createdCharacter);
        }

        // PUT: api/character/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            await _characterService.UpdateAsync(character);
            return NoContent();
        }

        // DELETE: api/character/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _characterService.DeleteAsync(id);
            return NoContent();
        }
    }

}
