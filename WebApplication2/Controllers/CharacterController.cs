using AnimeWebAPI.Models;
using AnimeWebAPI.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWebAPI.Controllers
{
    [ApiController]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET: api/v{version}/character
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Obsolete]
        public IActionResult GetAllAsyncV1()
        {
            // return any integer value
            return Ok(123);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetAllAsyncV2()
        {
            // return any string value
            return Ok("This is version 2.0");
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> GetAllAsyncV3()
        {
            // generate an Excel file and return it

            var characters = await _characterService.GetAllAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Characters");

            // Header
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Role";
            worksheet.Cell(1, 4).Value = "Anime ID";

            // Data
            int i = 2;
            foreach (var character in characters)
            {
                worksheet.Cell(i, 1).Value = character.Id;
                worksheet.Cell(i, 2).Value = character.Name;
                worksheet.Cell(i, 3).Value = character.Role;
                worksheet.Cell(i, 4).Value = character.AnimeId;
                i++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Characters.xlsx");
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
