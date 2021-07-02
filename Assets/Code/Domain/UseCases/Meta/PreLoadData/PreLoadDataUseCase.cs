using System.Threading.Tasks;
using Domain.Services.Server;

namespace Domain.UseCases.Meta.PreLoadData
{
    public class PreLoadDataUseCase : DataPreLoader
    {
        private readonly DataPreLoaderService _dataPreLoaderService;

        public PreLoadDataUseCase(DataPreLoaderService dataPreLoaderService)
        {
            _dataPreLoaderService = dataPreLoaderService;
        }

        public async Task PreLoad()
        {
            await _dataPreLoaderService.PreLoad();
        }
    }
}