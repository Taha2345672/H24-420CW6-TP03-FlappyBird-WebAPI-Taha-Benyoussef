namespace WebAPI.Models
{
    public class Scores
    {

        public int Id { get; set; }
        public string? Pseudo { get; set; }
        public DateTime Date { get; set; }
        public int TimeInSeconds { get; set; }
        public int ScoreValue { get; set; }
        public bool IsPublic { get; set; }

    }
}
