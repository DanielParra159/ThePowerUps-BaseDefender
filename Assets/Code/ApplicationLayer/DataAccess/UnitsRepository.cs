using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.Server;
using ApplicationLayer.Services.Server.Dtos.User;
using ApplicationLayer.Services.Server.Gateways.Catalog;
using ApplicationLayer.Services.Server.Gateways.Inventory;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using Domain.DataAccess;
using Domain.Entities;
using Domain.Services.Server;
using PlayFab.ServerModels;
using UnityEngine.Assertions;

namespace ApplicationLayer.DataAccess
{
    public partial class UnitsRepository : UnitsDataAccess, DataPreLoaderService
    {
        private readonly UserDataAccess _userDataAccess;
        private readonly Gateway _titleDataGateway;
        private readonly CatalogGateway _catalogGateway;
        private readonly InventoryGateway _inventoryGateway;

        private List<Unit> _units;
        private List<UserUnit> _userUnits;

        public UnitsRepository(UserDataAccess userDataAccess,
            Gateway titleDataGateway,
            CatalogGateway catalogGateway,
            InventoryGateway inventoryGateway)
        {
            _userDataAccess = userDataAccess;
            _titleDataGateway = titleDataGateway;
            _catalogGateway = catalogGateway;
            _inventoryGateway = inventoryGateway;
        }

        public IReadOnlyList<Unit> GetAllUnits()
        {
            Assert.IsNotNull(_units, "You need to call PreLoad first");
            return _units;
        }


        public IReadOnlyList<UserUnit> GetAllUserUnits()
        {
            Assert.IsNotNull(_userUnits, "You need to call PreLoad first");
            return _userUnits;
        }

        public UserUnit GetUserUnit(string id)
        {
            Assert.IsNotNull(_userUnits, "You need to call PreLoad first");
            return _userUnits.First(userUnit => userUnit.UnitId.Equals(id));
        }

        public IReadOnlyList<string> GetInitialUnitsId()
        {
            var initialUnitsDto = _titleDataGateway.Get<InitialUnitsDto>();
            return initialUnitsDto.UnitsId;
        }
    }
}