using System.Threading.Tasks;

namespace Domain.Services.SceneHandler
{
    public interface SceneLoaderService
    {
        Task Load(string sceneName);
    }
}