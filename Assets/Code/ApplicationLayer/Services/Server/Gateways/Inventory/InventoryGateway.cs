using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.User;
using PlayFab.ServerModels;

namespace ApplicationLayer.Services.Server.Gateways.Inventory
{
    public interface InventoryGateway
    {
        Task<IReadOnlyList<InventoryItemDto>> GrantUnitsToUser(string userId, List<ItemGrant> units);
        Task<IReadOnlyList<InventoryItemDto>> GetUserInventory(string userId);
        Task RevokeUserItem(List<RevokeInventoryItem> itemsToRevoke);

    }
}