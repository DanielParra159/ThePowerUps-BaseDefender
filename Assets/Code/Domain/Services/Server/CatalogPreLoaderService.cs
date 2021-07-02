using System.Threading.Tasks;

namespace Domain.Services.Server
{
    public interface CatalogPreLoaderService
    {
        Task PreLoad<T>(string catalogId);
    }
}