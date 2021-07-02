using System;
using System.Threading.Tasks;
using Domain.Services.Server;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public abstract class PlayFabLogin : AuthenticateService
    {
        public string UserId { get; private set; }

        public Task Authenticate()
        {
            var t = new TaskCompletionSource<bool>();

            Login(t);
            
            return Task.Run(() => t.Task);
        }

        protected abstract void Login(TaskCompletionSource<bool> taskCompletionSource);

        protected void OnError(PlayFabError error, TaskCompletionSource<bool> taskCompletionSource)
        {
            taskCompletionSource.SetResult(false);
            throw new Exception(error.GenerateErrorReport());
        }

        protected void OnSuccess(LoginResult result, TaskCompletionSource<bool> taskCompletionSource)
        {
            UserId = result.PlayFabId;
            taskCompletionSource.SetResult(true);
            Debug.Log("Login");
        }
    }
}
