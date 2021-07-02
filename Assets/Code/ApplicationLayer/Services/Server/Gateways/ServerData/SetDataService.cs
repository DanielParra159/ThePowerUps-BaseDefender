using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Server.Gateways.ServerData
{
    public interface SetDataService
    {
        Task Set(Dictionary<string, string> data);
    }
}
