using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabSetUserDataService : SetDataService
    {
        public Task Set(Dictionary<string, string> data)
        {
            var t = new TaskCompletionSource<bool>();
            UpdateData(data, t);
            return Task.Run(() => t.Task);
        }

        private static void UpdateData(Dictionary<string, string> data, TaskCompletionSource<bool> t)
        {
            var request = new UpdateUserDataRequest
            {
                Data = data
            };
            PlayFabClientAPI
                .UpdateUserData(request,
                    result =>
                    {
                        Debug.Log("UpdateUserData success");
                        t.SetResult(true);
                    },
                    error =>
                    {
                        Debug.Log(
                            "UpdateUserData error: " + error.GenerateErrorReport());
                        t.SetCanceled();
                    });
        }
    }
}