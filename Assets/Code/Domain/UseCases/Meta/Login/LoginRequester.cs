using System.Threading.Tasks;

namespace Domain.UseCases.Meta.Login
{
    public interface LoginRequester
    {
        Task Login();
    }
}
