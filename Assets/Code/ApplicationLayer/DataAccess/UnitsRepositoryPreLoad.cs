using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.Server;
using ApplicationLayer.Services.Server.Dtos.User;
using Domain.Entities;

namespace ApplicationLayer.DataAccess
{
    public partial class UnitsRepository
    {
        public async Task PreLoad()
        {
            GetUnitsFromServer();
            await GetUserUnitsFromServer();
        }

        private void GetUnitsFromServer()
        {
            var unitsDtos = _catalogGateway.GetItems("Units");
            _units = ParseUnits(unitsDtos);
        }

        private static List<Unit> ParseUnits(IReadOnlyList<CatalogItemDto> unitsDtos)
        {
            return new List<Unit>(unitsDtos
                .Select(UnitMapper.ParseUnits));
        }

        private async Task GetUserUnitsFromServer()
        {
            var userInventory = await _inventoryGateway.GetUserInventory(_userDataAccess.GetUserId());
            _userUnits = ParseInventory(userInventory);
        }

        private List<UserUnit> ParseInventory(IReadOnlyList<InventoryItemDto> userInventory)
        {
            return new List<UserUnit>(userInventory
                .Select(userUnitDto =>
                {
                    var unit = _units.First(unit => unit.Id.Equals(userUnitDto.ItemId));
                    return UnitMapper.InventoryItemToUserUnit(unit, userUnitDto);
                }));
        }
    }
}