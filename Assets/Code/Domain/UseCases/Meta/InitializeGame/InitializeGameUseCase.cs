using Domain.UseCases.Meta.CreateUser;
using Domain.UseCases.Meta.LoadScene;
using Domain.UseCases.Meta.Login;
using Domain.UseCases.Meta.PreLoadData;

namespace Domain.UseCases.Meta.InitializeGame
{
    public class InitializeGameUseCase : GameInitializer
    {
        private readonly LoginRequester _loginRequester;
        private readonly DataPreLoader _dataPreLoader;
        private readonly UserInitializer _userInitializer;
        private readonly SceneLoader _sceneLoader;

        public InitializeGameUseCase(LoginRequester loginRequester,
            DataPreLoader dataPreLoader,
            UserInitializer userInitializer, 
            SceneLoader sceneLoader)
        {
            _loginRequester = loginRequester;
            _dataPreLoader = dataPreLoader;
            _userInitializer = userInitializer;
            _sceneLoader = sceneLoader;
        }

        public async void InitGame()
        {
            await _loginRequester.Login();
            await _dataPreLoader.PreLoad();
            _userInitializer.Init();

            await _sceneLoader.Load("Menu", false);
        }
    }
}