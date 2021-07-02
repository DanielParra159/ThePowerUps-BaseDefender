namespace Domain.UseCases.Meta.Loading
{
    public class HideLoadingUseCase : LoadingHide
    {
        private readonly HideLoadingOutputBoundary _hideLoadingOutputBoundary;

        public HideLoadingUseCase(HideLoadingOutputBoundary hideLoadingOutputBoundary)
        {
            _hideLoadingOutputBoundary = hideLoadingOutputBoundary;
        }


        public void Hide()
        {
            _hideLoadingOutputBoundary.Hide();   
        }
    }
}