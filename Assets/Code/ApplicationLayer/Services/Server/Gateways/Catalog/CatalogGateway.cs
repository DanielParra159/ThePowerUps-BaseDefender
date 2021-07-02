using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.Server;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface CatalogGateway
    {
        IReadOnlyList<CatalogItemDto> GetItems(string catalogId);
    }
}
