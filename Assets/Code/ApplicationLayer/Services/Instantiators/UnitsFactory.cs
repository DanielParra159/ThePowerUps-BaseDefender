using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemUtilities;
using ApplicationLayer.DataAccess;
using Code.View.Units;
using Domain.Services.Server;
using UnityEngine;

namespace ApplicationLayer.Services.Instantiators
{
    public class UnitsFactory : DataPreLoaderService
    {
        private readonly UnitsConfiguration _configuration;
        
        public UnitsFactory(UnitsConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task PreLoad()
        {
            await LoadAllAssets();
            _configuration.CreateMapper();
        }

        private async Task LoadAllAssets()
        {
            var tasks = new List<Task>(_configuration.UnitPrefabs.Length);
            tasks.AddRange(
                _configuration.UnitPrefabs
                    .Select(unitAssetReference
                        => unitAssetReference.LoadAssetAsyncAsATask<GameObject>())
            );
            await Task.WhenAll(tasks);
        }

        public async Task<UnitView> Create(UnitConfiguration unitConfiguration)
        {
            var assetReference = _configuration.GetUnit(unitConfiguration.Id);
            var unit = await assetReference.InstantiateAsyncAsATask<UnitView>(unitConfiguration.Position);
            return unit;
        }
    }
}