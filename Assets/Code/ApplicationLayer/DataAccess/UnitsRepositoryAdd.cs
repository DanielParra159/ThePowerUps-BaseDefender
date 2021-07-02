using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.User;
using Domain.DataAccess;
using Domain.Entities;
using PlayFab.ServerModels;

namespace ApplicationLayer.DataAccess
{
    public partial class UnitsRepository
    {
        public async Task<IReadOnlyList<UserUnit>> AddUnitsToUser(List<UnitToAdd> units)
        {
            var items = GetItemsToGrant(units);
            var grantUnitsToUser = await _inventoryGateway.GrantUnitsToUser(_userDataAccess.GetUserId(), items);
            SaveNewUserUnits(grantUnitsToUser);

            return _userUnits;
        }

        private List<ItemGrant> GetItemsToGrant(List<UnitToAdd> units)
        {
            var items = new List<ItemGrant>(units
                .Select(unit => new ItemGrant
                    {
                        Annotation = unit.Annotation,
                        ItemId = unit.Id,
                        Data = UnitMapper.ParseFieldsToDictionary(unit.UnitState),
                        PlayFabId = _userDataAccess.GetUserId()
                    }
                ));
            return items;
        }

        private void SaveNewUserUnits(IReadOnlyList<InventoryItemDto> grantUnitsToUser)
        {
            _userUnits.AddRange(new List<UserUnit>(grantUnitsToUser
                .Select(userUnitDto =>
                {
                    var unit = _units.First(unit => unit.Id.Equals(userUnitDto.ItemId));
                    return UnitMapper.InventoryItemToUserUnit(unit, userUnitDto);
                })));
        }
    }
}