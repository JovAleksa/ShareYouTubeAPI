using ShareYouTubeAPI.Models;

namespace ShareYouTubeAPI.IRepository
{
    public interface ILikeRepository
    {
        IQueryable<Like> GetAll();
        Like GetById(int id);
        void Add(Like like);
        void Update(Like like);
        void Delete(Like like);
    }
}
