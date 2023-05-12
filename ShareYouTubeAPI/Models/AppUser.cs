using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]        
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public string? ChannelDescritpion { get; set; }
        public DateTime DateRegistered { get; set; }
        public string? Role { get; set; }
        public bool Status { get; set; }
        public int ListUsersId { get; set; }
        public List<ListUsers> Subscribers { get; set; }

      
    }
}
