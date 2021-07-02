using System.Threading.Tasks;

namespace Domain.Services.Server
{
    public interface DataPreLoaderService
    {
        Task PreLoad();
    }
}