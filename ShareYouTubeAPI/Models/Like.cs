using System.ComponentModel.DataAnnotations;

namespace ShareYouTubeAPI.Models
{
    public class Like
    {
        public int Id { get; set; }
        [Required]
        public bool IsLike { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }


        // Like/dislike
        //1. ID(jedinstven, generiše aplikacija)
        //2. Da li je like? (true/false) (generiše aplikacija)
        //3. Datum(vreme) kreiranja(generiše aplikacija)
        //4. Video ili komentar(koji se like-uje/dislike-uje, povezuje aplikacija)
    }
}
