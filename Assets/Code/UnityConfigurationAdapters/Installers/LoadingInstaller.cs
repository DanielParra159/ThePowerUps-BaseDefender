using SystemUtilities;
using Code.InterfaceAdapters.Loading;
using Code.View.UI;
using Domain.UseCases.Meta.Loading;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class LoadingInstaller : MonoBehaviour
    {
        [SerializeField] private LoadingView _loadingView;

        public void Install()
        {
            var loadingViewModel = new LoadingViewModel();
            _loadingView.SetModel(loadingViewModel);
            var loadingPresenter = new LoadingPresenter(loadingViewModel);

            var showLoadingUseCase = new ShowLoadingUseCase(loadingPresenter);
            var hideLoadingUseCase = new HideLoadingUseCase(loadingPresenter);
            
            ServiceLocator.Instance.RegisterService<LoadingShow>(showLoadingUseCase);
            ServiceLocator.Instance.RegisterService<LoadingHide>(hideLoadingUseCase);
        }
    }
}