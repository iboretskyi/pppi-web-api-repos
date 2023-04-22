namespace AnimeWebAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
    }
}
