using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace ApplicationLayer.Services.Server.Gateways.ServerData
{
    public interface GetDataService
    {
        Task<Optional<DataResult>> Get(List<string> keys);
    }
}
