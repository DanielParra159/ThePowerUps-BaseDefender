using System.Threading.Tasks;

namespace Domain.Services.Server
{
    public interface AuthenticateService
    {
        public string UserId { get; }

        Task Authenticate();
    }
}
