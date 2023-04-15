using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
