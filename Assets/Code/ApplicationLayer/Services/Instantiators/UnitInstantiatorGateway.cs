using System.Threading.Tasks;
using ApplicationLayer.DataAccess;

namespace ApplicationLayer.Services.Instantiators
{
    public interface UnitInstantiatorGateway
    {
        Task<int> Instantiate(UnitConfiguration unitConfiguration);
    }
}