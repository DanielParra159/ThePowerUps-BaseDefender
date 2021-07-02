using System.Threading.Tasks;
using Domain.Services.Server;

namespace Domain.UseCases.Meta.Login
{
    public class LoginUseCase : LoginRequester
    {
        private readonly AuthenticateService _authenticateService;

        public LoginUseCase(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        public async Task Login()
        {
            await _authenticateService.Authenticate();
        }
    }
}
