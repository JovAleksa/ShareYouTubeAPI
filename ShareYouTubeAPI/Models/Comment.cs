using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models
{
    public class Comment
    {

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        [Required]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        [Required]
        public int VideoId { get; set; }
        public Video Video { get; set; }


        //        Komentar
        //1. ID(jedinstven, generiše aplikacija)
        //2. Sadržaj
        //3. Datum(vreme) kreiranja(generiše aplikacija)
        //4. Vlasnik(korisnik, povezuje aplikacija)
        //5. Video(na koji je ovo komentar, povezuje aplikacija)
    }
}
