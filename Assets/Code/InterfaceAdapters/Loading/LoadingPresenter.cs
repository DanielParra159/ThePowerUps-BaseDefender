using Domain.UseCases.Meta.Loading;

namespace Code.InterfaceAdapters.Loading
{
    public class LoadingPresenter : ShowLoadingOutputBoundary, HideLoadingOutputBoundary
    {
        private readonly LoadingViewModel _model;

        public LoadingPresenter(LoadingViewModel model)
        {
            _model = model;
        }

        public void Show()
        {
            _model.IsVisible.Value = true;
        }

        public void Hide()
        {
            _model.IsVisible.Value = false;
        }
    }
}