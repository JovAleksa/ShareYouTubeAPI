using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models.DTO
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
       
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
       
        public string Email { get; set; }
        public string ChannelDescritpion { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
       
        public List<ListUsers> Subscribers { get; set; }
    }
}
