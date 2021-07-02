using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using PlayFab.ServerModels;

namespace ApplicationLayer.DataAccess
{
    public partial class UnitsRepository
    {
        public async Task RemoveUnitsToUser(List<UserUnit> unitsToRemove)
        {
            var revokeInventoryItems = GetItemsToRevoke(unitsToRemove);
            await _inventoryGateway.RevokeUserItem(revokeInventoryItems);

            RemoveUserUnits(unitsToRemove);
        }

        private List<RevokeInventoryItem> GetItemsToRevoke(List<UserUnit> unitsToRemove)
        {
            var revokeInventoryItems = new List<RevokeInventoryItem>(unitsToRemove
                .Select(userUnit => new RevokeInventoryItem
                {
                    ItemInstanceId = userUnit.InstanceId,
                    PlayFabId = _userDataAccess.GetUserId()
                }));
            return revokeInventoryItems;
        }

        private void RemoveUserUnits(List<UserUnit> unitsToRemove)
        {
            _userUnits.RemoveAll(unit =>
                unitsToRemove.Exists(unitToRemove => unit.InstanceId.Equals(unitToRemove.InstanceId)));
        }
    }
}