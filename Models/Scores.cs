using FlappyBird.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [ForeignKey("User")] 
        public string UserId { get; set; } = null!;
        
        [JsonIgnore]
        public virtual User User { get; set; } = null!;


    }
}
