using System.ComponentModel.DataAnnotations;


namespace WebAPI.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }


    }

}
