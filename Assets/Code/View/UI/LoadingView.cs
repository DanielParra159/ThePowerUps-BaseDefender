using Code.InterfaceAdapters.Loading;
using UniRx;

namespace Code.View.UI
{
    public class LoadingView : ViewBase
    {
        private LoadingViewModel _model;

        public void SetModel(LoadingViewModel model)
        {
            _model = model;
            _model
                .IsVisible
                .Subscribe(UpdateVisibility)
                .AddTo(_disposables);
        }

        private void UpdateVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}