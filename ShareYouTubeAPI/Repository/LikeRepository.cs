using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShareYouTubeAPI.IRepository;
using ShareYouTubeAPI.Models;
using System.Linq;

namespace ShareYouTubeAPI.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;
        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Like like)
        {
            _context.Add<Like>(like);
            _context.SaveChanges();
        }

        public void Delete(Like like)
        {
            _context.Remove(like);
            _context.SaveChanges();
        }

        public IQueryable<Like> GetAll()
        {
            return _context.Likes;
        }

        public Like GetById(int id)
        {
            return _context.Likes.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Like like)
        {
            _context.Entry<Like>(like).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
