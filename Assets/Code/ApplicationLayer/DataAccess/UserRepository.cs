using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Dtos.User;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using Domain.DataAccess;
using Domain.Entities;
using Domain.Services.Server;

namespace ApplicationLayer.DataAccess
{
    public class UserRepository : UserDataAccess
    {
        private readonly AuthenticateService _authenticateService;
        private readonly Gateway _userDataGateway;
        private User _localUser;

        public UserRepository(Gateway userDataGateway, AuthenticateService authenticateService)
        {
            _userDataGateway = userDataGateway;
            _authenticateService = authenticateService;
        }

        public bool IsNewUser()
        {
            var isNewUser = !_userDataGateway.Contains<IsInitializedDto>();
            if (isNewUser)
            {
                return true;
            }

            var isInitialized = _userDataGateway.Get<IsInitializedDto>().IsInitialized;
            return !isInitialized;
        }

        public User GetLocalUser()
        {
            if (_localUser != null)
            {
                return _localUser;
            }

            var isInitialized = _userDataGateway.Contains<IsInitializedDto>();
            _localUser = new User(isInitialized);
            return _localUser;
        }

        public string GetUserId()
        {
            return _authenticateService.UserId;
        }

        public Task CreateLocalUser()
        {
            _localUser = new User(true);
            _userDataGateway.Set(new IsInitializedDto(true));
            return _userDataGateway.Save();
        }
    }
}