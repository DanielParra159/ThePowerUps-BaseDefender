namespace Domain.UseCases.Meta.Loading
{
    public class ShowLoadingUseCase : LoadingShow
    {
        private readonly ShowLoadingOutputBoundary _showLoadingOutputBoundary;

        public ShowLoadingUseCase(ShowLoadingOutputBoundary showLoadingOutputBoundary)
        {
            _showLoadingOutputBoundary = showLoadingOutputBoundary;
        }

        public void Show()
        {
            _showLoadingOutputBoundary.Show();   
        }
    }
}