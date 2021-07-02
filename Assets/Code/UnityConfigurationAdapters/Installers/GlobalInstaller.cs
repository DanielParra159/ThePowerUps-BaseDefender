using System;
using SystemUtilities;
using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.EventDispatcher;
using ApplicationLayer.Services.SceneHandler;
using ApplicationLayer.Services.Serializer;
using ApplicationLayer.Services.Server;
using ApplicationLayer.Services.Server.Gateways.Catalog;
using ApplicationLayer.Services.Server.Gateways.Inventory;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using ApplicationLayer.Services.Server.PlayFab;
using Code.Cheats;
using Domain.DataAccess;
using Domain.Services.EventDispatcher;
using Domain.UseCases.Meta.CreateUser;
using Domain.UseCases.Meta.InitializeGame;
using Domain.UseCases.Meta.Loading;
using Domain.UseCases.Meta.LoadScene;
using Domain.UseCases.Meta.Login;
using Domain.UseCases.Meta.PreLoadData;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Installers
{
    public class GlobalInstaller : MonoBehaviour
    {
        [SerializeField] private LoadingInstaller _loadingInstaller;
        [SerializeField] private Canvas _globalCanvas;

        public void Awake()
        {
            var serviceLocator = ServiceLocator.Instance;

            DontDestroyOnLoad(_globalCanvas);
            var loadingShow = InstallLoading(serviceLocator);
            var loadingHide = serviceLocator.GetService<LoadingHide>();

            var eventDispatcherServiceImpl = new EventDispatcherServiceImpl();

            var playFabLogin = GetPlayFabLogin();
            var loginUseCase = new LoginUseCase(playFabLogin);
            var unityJsonSerializer = new UnityJsonSerializer();
            var playFabGetUserDataService = new PlayFabGetUserDataService();
            var playFabGetTitleDataService = new PlayFabGetTitleDataService();
            var titleDataGateway = new TitleDataGateway(unityJsonSerializer, playFabGetTitleDataService, null);
            var playFabSetUserDataService = new PlayFabSetUserDataService();
            var userDataGateway = new UserDataGateway(unityJsonSerializer,
                playFabGetUserDataService,
                playFabSetUserDataService);
            var unitMapper = new UnitMapper(unityJsonSerializer);
            var userDataAccess = new UserRepository(userDataGateway, playFabLogin);
            var playFabCatalogService = new PlayFabCatalogService();
            var catalogGatewayImpl = new PlayFabCatalogGateway(playFabCatalogService);
            var playFabUserInventoryService = new PlayFabUserInventoryService();
            var inventoryGatewayImpl =
                new PlayFabInventoryGateway(playFabUserInventoryService, playFabUserInventoryService,
                    playFabUserInventoryService);
            var unitsRepository = new UnitsRepository(userDataAccess, titleDataGateway, catalogGatewayImpl,
                inventoryGatewayImpl);
            var dataPreLoaderServiceImpl =
                new DataPreLoaderServiceImpl(titleDataGateway, userDataGateway, catalogGatewayImpl, unitsRepository);
            var preLoadDataUseCase = new PreLoadDataUseCase(dataPreLoaderServiceImpl);
            var initializeUserUseCase = new InitializeUserUseCase(userDataAccess, unitsRepository);
            var unitySceneLoaderService = new UnitySceneLoaderService();
            var loadSceneUseCase = new LoadSceneUseCase(unitySceneLoaderService, loadingShow, loadingHide);
            var initializeGameUseCase
                = new InitializeGameUseCase(
                    loginUseCase,
                    preLoadDataUseCase,
                    initializeUserUseCase,
                    loadSceneUseCase
                );

            serviceLocator.RegisterService<UserInitializer>(initializeUserUseCase);
            serviceLocator.RegisterService<UnitsDataAccess>(unitsRepository);
            serviceLocator.RegisterService<RemoveItemsService>(playFabUserInventoryService);
            serviceLocator.RegisterService<EventDispatcherService>(eventDispatcherServiceImpl);
            serviceLocator.RegisterService<SceneLoader>(loadSceneUseCase);
            serviceLocator.RegisterService(userDataGateway);
            // SRDebug.Instance.AddOptionContainer(GlobalCheats.Instance);

            initializeGameUseCase.InitGame();
        }

        private LoadingShow InstallLoading(ServiceLocator serviceLocator)
        {
            _loadingInstaller.Install();
            var loadingShow = serviceLocator.GetService<LoadingShow>();
            loadingShow.Show();
            return loadingShow;
        }

        private static PlayFabLogin GetPlayFabLogin()
        {
#if UNITY_EDITOR
            return new PlayFabLoginEditor();
#elif UNITY_ANDROID
            return new PlayFabLoginAndroid();
#elif UNITY_IOS
            return new PlayFabLoginIos();
#endif
            throw new Exception("Platform not defined");
        }
    }
}