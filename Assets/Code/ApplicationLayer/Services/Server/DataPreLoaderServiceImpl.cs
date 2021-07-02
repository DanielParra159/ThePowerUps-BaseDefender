using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.Server;
using Domain.Services.Server;

namespace ApplicationLayer.Services.Server
{
    public class DataPreLoaderServiceImpl : DataPreLoaderService
    {
        private readonly DataPreLoaderService _serverDataPreLoader;
        private readonly DataPreLoaderService _clientDataPreLoader;
        private readonly CatalogPreLoaderService _catalogDataPreLoader;
        private readonly DataPreLoaderService _unitsRepository;

        public DataPreLoaderServiceImpl(DataPreLoaderService serverDataPreLoader,
            DataPreLoaderService clientDataPreLoader,
            CatalogPreLoaderService catalogDataPreLoader,
            DataPreLoaderService unitsRepository)
        {
            _serverDataPreLoader = serverDataPreLoader;
            _clientDataPreLoader = clientDataPreLoader;
            _catalogDataPreLoader = catalogDataPreLoader;
            _unitsRepository = unitsRepository;
        }

        public async Task PreLoad()
        {
            await _serverDataPreLoader.PreLoad();
            await _clientDataPreLoader.PreLoad();
            await _catalogDataPreLoader.PreLoad<UnitCustomData>("Units");
            await _unitsRepository.PreLoad();
        }
    }
}