using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab.ServerModels;

namespace ApplicationLayer.Services.Server.Gateways.Inventory
{
    public interface GrantItemsService
    {
        Task<List<GrantedItemInstance>> GrantItemsToUsers(string catalogId, List<ItemGrant> itemGrant);
    }
}