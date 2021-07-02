using System.Threading.Tasks;
using Domain.Services.SceneHandler;
using Domain.UseCases.Meta.Loading;

namespace Domain.UseCases.Meta.LoadScene
{
    public class LoadSceneUseCase : SceneLoader
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly LoadingShow _loadingShow;
        private readonly LoadingHide _loadingHide;

        public LoadSceneUseCase(SceneLoaderService sceneLoaderService, 
            LoadingShow loadingShow,
            LoadingHide loadingHide)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingShow = loadingShow;
            _loadingHide = loadingHide;
        }

        public async Task Load(string sceneName, bool hideLoading)
        {
            _loadingShow.Show();
            
            await _sceneLoaderService.Load(sceneName);
            
            if (hideLoading)
            {
                _loadingHide.Hide();
            }
        }
    }
}