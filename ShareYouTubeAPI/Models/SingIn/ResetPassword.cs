using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models.SingIn
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "The password and confirm password do not match!")]
        public string ConfrimPassword { get; set; } = null!;
        public string  Token { get; set; }
        public string Email { get; set; }
    }
}
