using ShareYouTubeAPI.Models;

namespace ShareYouTubeAPI.IRepository
{
    public interface IVideoRepository
    {
        IQueryable<Video> GetAll();
        Video GetById(int id);
        IQueryable<Video> GetByName(string id);
        void Add(Video video);
        void Update(Video video);
        void Delete(Video video);

    }
}
