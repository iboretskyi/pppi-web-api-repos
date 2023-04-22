using AnimeWebAPI.Models;

namespace AnimeWebAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly List<Character> _characters = new();

        public CharacterService()
        {
            _characters.AddRange(new List<Character>
            {
                new Character { Id = 1, Name = "Kaneda Shotaro", Role = "Main", AnimeId = 1 },
                new Character { Id = 2, Name = "Tetsuo Shima", Role = "Main", AnimeId = 1 },
                new Character { Id = 3, Name = "Son Goku", Role = "Main", AnimeId = 2 },
                new Character { Id = 4, Name = "Bulma", Role = "Main", AnimeId = 2 },
                new Character { Id = 5, Name = "Usagi Tsukino", Role = "Main", AnimeId = 3 },
                new Character { Id = 6, Name = "Ami Mizuno", Role = "Main", AnimeId = 3 },
                new Character { Id = 7, Name = "Shinji Ikari", Role = "Main", AnimeId = 4 },
                new Character { Id = 8, Name = "Rei Ayanami", Role = "Main", AnimeId = 4 },
                new Character { Id = 9, Name = "Motoko Kusanagi", Role = "Main", AnimeId = 5 },
                new Character { Id = 10, Name = "Batou", Role = "Main", AnimeId = 5 },
                new Character { Id = 11, Name = "Spike Spiegel", Role = "Main", AnimeId = 6 },
                new Character { Id = 12, Name = "Faye Valentine", Role = "Main", AnimeId = 6 },
                new Character { Id = 13, Name = "Amuro Ray", Role = "Main", AnimeId = 7 },
                new Character { Id = 14, Name = "Char Aznable", Role = "Main", AnimeId = 7 },
                new Character { Id = 15, Name = "Ranma Saotome", Role = "Main", AnimeId = 8 },
                new Character { Id = 16, Name = "Akane Tendo", Role = "Main", AnimeId = 8 },
                new Character { Id = 17, Name = "Lina Inverse", Role = "Main", AnimeId = 9 },
                new Character { Id = 18, Name = "Gourry Gabriev", Role = "Main", AnimeId = 9 },
                new Character { Id = 19, Name = "Nadia", Role = "Main", AnimeId = 10 },
                new Character { Id = 20, Name = "Jean", Role = "Main", AnimeId = 10 }
            });
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return _characters;
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return _characters.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Character> AddAsync(Character character)
        {
            _characters.Add(character);
            return character;
        }

        public async Task UpdateAsync(Character character)
        {
            var existingCharacter = _characters.FirstOrDefault(c => c.Id == character.Id);
            if (existingCharacter != null)
            {
                existingCharacter.Name = character.Name;
                existingCharacter.Role = character.Role;
                existingCharacter.AnimeId = character.AnimeId;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            if (character != null)
            {
                _characters.Remove(character);
            }
        }
    }
}
