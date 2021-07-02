using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginIos : PlayFabLogin
    {
        protected override void Login(TaskCompletionSource<bool> taskCompletionSource)
        {
            var request = new LoginWithIOSDeviceIDRequest
                          {DeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
            PlayFabClientAPI.LoginWithIOSDeviceID(request, result => OnSuccess(result, taskCompletionSource),
                                                  error => OnError(error, taskCompletionSource));
        }
    }
}
