using UniRx;

namespace Code.InterfaceAdapters.Loading
{
    public class LoadingViewModel
    {
        public readonly ReactiveProperty<bool> IsVisible;

        public LoadingViewModel()
        {
            IsVisible = new BoolReactiveProperty();
        }
    }
}