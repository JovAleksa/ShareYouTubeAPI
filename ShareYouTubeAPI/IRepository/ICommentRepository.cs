using ShareYouTubeAPI.Models;

namespace ShareYouTubeAPI.IRepository
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetAll();
        Comment GetById(int id);
        IQueryable<Comment> GetByUserId(string id);
        IQueryable<Comment> GetByVideoId(string id);
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
