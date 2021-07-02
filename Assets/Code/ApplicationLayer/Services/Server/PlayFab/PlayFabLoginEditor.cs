using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginEditor : PlayFabLogin
    {
        protected override void Login(TaskCompletionSource<bool> taskCompletionSource)
        {
            var request = new LoginWithCustomIDRequest
                          {
                              CreateAccount = true,
                              CustomId = "1"
                          };


            PlayFabClientAPI
               .LoginWithCustomID(request,
                                  result => OnSuccess(result, taskCompletionSource),
                                  error => OnError(error, taskCompletionSource)
                                 );
        }
    }
}
