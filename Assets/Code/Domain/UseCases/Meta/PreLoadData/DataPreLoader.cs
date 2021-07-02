using System.Threading.Tasks;

namespace Domain.UseCases.Meta.PreLoadData
{
    public interface DataPreLoader
    {
        Task PreLoad();
    }
}