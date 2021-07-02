using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginAndroid : PlayFabLogin
    {

        protected override void Login(TaskCompletionSource<bool> taskCompletionSource)
        {
            var request = new LoginWithAndroidDeviceIDRequest
                          {
                              AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
                              CreateAccount = true
                          };
            PlayFabClientAPI.LoginWithAndroidDeviceID(request, result => OnSuccess(result, taskCompletionSource),
                                                      error => OnError(error, taskCompletionSource));

        }
    }
}
