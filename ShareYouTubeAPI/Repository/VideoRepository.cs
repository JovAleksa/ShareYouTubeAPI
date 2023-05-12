using Microsoft.EntityFrameworkCore;
using ShareYouTubeAPI.IRepository;
using ShareYouTubeAPI.Models;
using System.Linq;

namespace ShareYouTubeAPI.Repository
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Video video)
        {
            _context.Add<Video>(video);
            _context.SaveChanges();
        }

        public void Delete(Video video)
        {
            _context.Remove(video);
            _context.SaveChanges();
        }

        public IQueryable<Video> GetAll()
        {
            return _context.Videos;
        }

        public Video GetById(int id)
        {
            return _context.Videos.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Video> GetByName(string id)
        {
           
           return _context.Videos.Where(p => p.Url.Contains(id)).OrderBy(p => p.Url);
            
        }

        public void Update(Video video)
        {
            _context.Entry<Video>(video).State = EntityState.Modified;
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
