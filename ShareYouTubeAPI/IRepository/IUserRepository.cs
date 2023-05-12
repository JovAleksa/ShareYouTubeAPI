using ShareYouTubeAPI.Models;

namespace ShareYouTubeAPI.IRepository
{
    public interface IUserRepository
    {
        IQueryable<AppUser> GetAll();
        AppUser GetById(int id);
        void Add(AppUser appuser);
        void Update(AppUser appuser);
        void Delete(AppUser appuser);
        IQueryable<AppUser> GetByName(string name);

        IQueryable<AppUser> SearchByVideoId(int idVideo);
    }
}
