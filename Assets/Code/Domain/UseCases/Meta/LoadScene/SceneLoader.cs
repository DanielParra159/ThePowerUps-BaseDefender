using System.Threading.Tasks;

namespace Domain.UseCases.Meta.LoadScene
{
    public interface SceneLoader
    {
        Task Load(string sceneName, bool hideLoading);
    }
}