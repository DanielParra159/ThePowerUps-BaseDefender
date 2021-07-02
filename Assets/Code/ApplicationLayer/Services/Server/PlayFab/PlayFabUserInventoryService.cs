using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Server.Gateways.Inventory;
using PlayFab.ServerModels;
using PlayFab;
using UnityEngine;

namespace ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabUserInventoryService : GrantItemsService, RemoveItemsService, GetInventoryService
    {
        public Task<List<GrantedItemInstance>> GrantItemsToUsers(string catalogId, List<ItemGrant> itemGrant)
        {
            var taskCompletionSource = new TaskCompletionSource<List<GrantedItemInstance>>();

            GrantItems(catalogId, itemGrant, taskCompletionSource);

            return Task.Run(() => taskCompletionSource.Task);
        }

        public Task RevokeItemsToUsers(List<RevokeInventoryItem> itemsToRevoke)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            RevokeItems(itemsToRevoke, taskCompletionSource);

            return Task.Run(() => taskCompletionSource.Task);
        }

        public Task<List<ItemInstance>> GetUserInventory(string userId)
        {
            var taskCompletionSource = new TaskCompletionSource<List<ItemInstance>>();
            GetInventory(taskCompletionSource, userId);

            return Task.Run(() => taskCompletionSource.Task);
        }

        private void GetInventory(TaskCompletionSource<List<ItemInstance>> taskCompletionSource, string userId)
        {
            var request = new GetUserInventoryRequest
            {
                PlayFabId = userId
            };
            PlayFabServerAPI
                .GetUserInventory(request,
                    result => OnGetInventorySuccess(result, taskCompletionSource),
                    error => OnError(error)
                );
        }

        private void OnGetInventorySuccess(GetUserInventoryResult result, TaskCompletionSource<List<ItemInstance>> taskCompletionSource)
        {
            taskCompletionSource.SetResult(result.Inventory);
            Debug.Log("Get inventory items");
        }

        private void RevokeItems(List<RevokeInventoryItem> itemsToRevoke,
            TaskCompletionSource<bool> taskCompletionSource)
        {
            var request = new RevokeInventoryItemsRequest
            {
                Items = itemsToRevoke
            };
            PlayFabServerAPI
                .RevokeInventoryItems(request,
                    result => OnRevokeSuccess(result, taskCompletionSource),
                    error => OnError(error)
                );
        }

        private void GrantItems(string catalogId, List<ItemGrant> itemGrant,
            TaskCompletionSource<List<GrantedItemInstance>> taskCompletionSource)
        {
            var request = new GrantItemsToUsersRequest
            {
                CatalogVersion = catalogId,
                ItemGrants = itemGrant,
            };
            PlayFabServerAPI
                .GrantItemsToUsers(request,
                    result => OnSuccess(result, taskCompletionSource),
                    error => OnError(error)
                );
        }

        private void OnError(PlayFabError error)
        {
            //taskCompletionSource.SetResult(false);
            throw new Exception(error.GenerateErrorReport());
        }

        private void OnRevokeSuccess(RevokeInventoryItemsResult result, TaskCompletionSource<bool> taskCompletionSource)
        {
            taskCompletionSource.SetResult(true);
            Debug.Log("Removed items");
        }

        private void OnSuccess(GrantItemsToUsersResult result,
            TaskCompletionSource<List<GrantedItemInstance>> taskCompletionSource)
        {
            taskCompletionSource.SetResult(result.ItemGrantResults);
            Debug.Log("Grant items");
        }
    }
}