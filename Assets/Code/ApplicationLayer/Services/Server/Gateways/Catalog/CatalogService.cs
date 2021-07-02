using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;
using PlayFab.ClientModels;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface CatalogService
    {
        Task<Optional<List<CatalogItem>>> GetItems(string catalogId);
    }
}