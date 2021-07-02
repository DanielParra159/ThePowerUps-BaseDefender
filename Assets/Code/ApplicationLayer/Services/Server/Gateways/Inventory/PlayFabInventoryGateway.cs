using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.DataAccess;
using ApplicationLayer.Services.Server.Dtos.User;
using Code.SharedTypes.Units;
using PlayFab.ServerModels;

namespace ApplicationLayer.Services.Server.Gateways.Inventory
{
    public class PlayFabInventoryGateway : InventoryGateway
    {
        private readonly GrantItemsService _grantItemsService;
        private readonly GetInventoryService _getInventoryService;
        private readonly RemoveItemsService _removeItemsService;


        public PlayFabInventoryGateway(GrantItemsService grantItemsService,
            GetInventoryService getInventoryService,
            RemoveItemsService removeItemsService)
        {
            _grantItemsService = grantItemsService;
            _getInventoryService = getInventoryService;
            _removeItemsService = removeItemsService;
        }

        public async Task<IReadOnlyList<InventoryItemDto>> GrantUnitsToUser(string userId,
            List<ItemGrant> units)
        {
            var grantItemsToUsers = await _grantItemsService.GrantItemsToUsers("Units", units);
            return grantItemsToUsers
                .Select(UnitMapper.ParseInventoryItem<UnitState>)
                .ToList();
        }

        public async Task<IReadOnlyList<InventoryItemDto>> GetUserInventory(string userId)
        {
            var grantItemsToUsers = await _getInventoryService.GetUserInventory(userId);
            return grantItemsToUsers
                .Select(UnitMapper.ParseInventoryItem<UnitState>)
                .ToList();
        }

        public async Task RevokeUserItem(List<RevokeInventoryItem> itemsToRevoke)
        {
            await _removeItemsService.RevokeItemsToUsers(itemsToRevoke);
        }
    }
}