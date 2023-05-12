using Microsoft.Extensions.Options;
using Org.BouncyCastle.Bcpg;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ShareYouTubeAPI.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public enum Visible
        {
            Public = 1,
            Unlisted =2,
            Private = 3
        } 
        [Required]
        public bool Comments { get; set; }
        [Required]
        public bool Rating { get; set; }
        [Required]
        public bool Blocked { get; set; }
        [Required]
        public int Reviews { get; set; }
        public DateTime DateCreate { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }

        //        Video
        //1. ID(jedinstven, generiše aplikacija)
        //2. Video(URL do stvarnog videa na web-u, najbolje na YouTube-u)
        //3. Sličica(thumbnail, URL do stvarne sličice negde na web-u ili do placeholder-a u paketu projekta, pri
        //čemu može biti ista za sve videe)
        //4. Opis(opcioni)
        //5. Vidljivost(javni, unlisted, privatni)
        //6. Dozvoljeni komentari(true/false)
        //7. Vidljivost rejting-a, tj.broja like-ova i broj dislike-ova(true/false)
        //8. Blokiran(true/false), samo za administratore
        //9. Broj pregleda(generiše aplikacija)
        //10. Datum(vreme) kreiranja(generiše aplikacija)
        //11. Vlasnik(korisnik, povezuje aplikacija)

    }
}
