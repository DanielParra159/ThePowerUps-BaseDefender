using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DataAccess
{
    public interface UserDataAccess
    {
        bool IsNewUser();
        User GetLocalUser();
        string GetUserId();
        Task CreateLocalUser();
    }
}
