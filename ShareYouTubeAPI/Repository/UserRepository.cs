using Microsoft.EntityFrameworkCore;
using ShareYouTubeAPI.IRepository;
using ShareYouTubeAPI.Models;
using System.Linq;

namespace ShareYouTubeAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AppUser appuser)
        {
            _context.Add<AppUser>(appuser);
            _context.SaveChanges();
        }

        public void Delete(AppUser appuser)
        {
            _context.Remove(appuser);
            _context.SaveChanges();
        }

        public IQueryable<AppUser> GetAll()
        {
            return _context.AppUsers;
        }

        public AppUser GetById(int id)
        {
            return _context.AppUsers.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<AppUser> GetByName(string name)
        {
            return _context.AppUsers.Where(p => p.Name.Contains(name)).OrderBy(p => p.Name);
        }

        public IQueryable<AppUser> SearchByVideoId(int idVideo)
        {
            return _context.AppUsers.Where(p => p.Id == idVideo).OrderBy(p => p.Id);
        }

        public void Update(AppUser appuser)
        {
            _context.Entry<AppUser>(appuser).State = EntityState.Modified;
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
