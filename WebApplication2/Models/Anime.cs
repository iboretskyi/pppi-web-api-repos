namespace AnimeWebAPI.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Character> Characters { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Genre: {Genre}, Release Date: {ReleaseDate.ToString("yyyy-MM-dd")}, Number of Characters: {Characters?.Count}";
        }
    }
}
