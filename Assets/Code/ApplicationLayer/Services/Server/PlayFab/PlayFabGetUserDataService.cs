using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;
using ApplicationLayer.Services.Server.Gateways.ServerData;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabGetUserDataService : GetDataService
    {
        public Task<Optional<DataResult>> Get(List<string> keys)
        {
            var t = new TaskCompletionSource<Optional<DataResult>>();

            var request = new GetUserDataRequest {Keys = keys};
            PlayFabClientAPI
               .GetUserData(request,
                            result => { SetResult(result, t); },
                            error =>
                            {
                                Debug.Log("GetTitleData error: " + error.GenerateErrorReport());
                                t.SetResult(new Optional<DataResult>());
                            });

            return Task.Run(() => t.Task);
        }

        private static void SetResult(GetUserDataResult result,
                                      TaskCompletionSource<Optional<DataResult>> t)
        {
            var resultData = new Dictionary<string, string>(result.Data.Count);
            foreach (var userDataRecord in result.Data)
            {
                resultData.Add(userDataRecord.Key, userDataRecord.Value.Value);
            }

            t.SetResult(new Optional<DataResult>(new DataResult(resultData)));
        }
    }
}
