using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using WebAPI.Models;



namespace FlappyBird.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public virtual List<Scores>? Scores { get; set; } = null;
    }
}
