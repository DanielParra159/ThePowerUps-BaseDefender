using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos;

namespace ApplicationLayer.Services.Server.Gateways.ServerData
{
    public interface Gateway
    {
        T Get<T>() where T : Dto;
        bool Contains<T>() where T : Dto;
        void Set<T>(T data) where T : Dto;
        Task Save();
    }
}
