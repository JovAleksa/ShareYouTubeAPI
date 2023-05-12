

using User.Manager.Service.Models;

namespace User.Manager.Service.Services
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
