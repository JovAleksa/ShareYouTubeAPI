using ShareYouTubeAPI.IRepository;
using ShareYouTubeAPI.Models;
using System.Linq;

namespace ShareYouTubeAPI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public void Add(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetByUserId(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetByVideoId(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
