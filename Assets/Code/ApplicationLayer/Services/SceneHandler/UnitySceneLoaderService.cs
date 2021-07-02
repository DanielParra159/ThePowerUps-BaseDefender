using System.Threading.Tasks;
using Domain.Services.SceneHandler;
using UnityEngine.SceneManagement;

namespace ApplicationLayer.Services.SceneHandler
{
    public class UnitySceneLoaderService : SceneLoaderService
    {
        public async Task Load(string sceneName)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }

            await Task.Yield();
        }
    }
}