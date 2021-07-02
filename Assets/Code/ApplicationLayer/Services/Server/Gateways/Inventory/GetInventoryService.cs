using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab.ServerModels;

namespace ApplicationLayer.Services.Server.Gateways.Inventory
{
    public interface GetInventoryService
    {
        Task<List<ItemInstance>> GetUserInventory(string userId);
    }
}