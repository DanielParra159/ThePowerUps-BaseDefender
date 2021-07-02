using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SystemUtilities
{
    public static class AssetReferenceExtension {
        public static Task<T> LoadAssetAsyncAsATask<T>(this AssetReference assetReference)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            var asyncOperationHandle = assetReference.LoadAssetAsync<T>();

            asyncOperationHandle.Completed += OnCompleted;
        
            void OnCompleted(AsyncOperationHandle<T> handle)
            {
                taskCompletionSource.SetResult(handle.Result);
                asyncOperationHandle.Completed -= OnCompleted;
            }
        
            return Task.Run(() => taskCompletionSource.Task);
        }
        
        public static async Task<T> InstantiateAsyncAsATask<T>(this AssetReference assetReference, Vector3 position)
        {
            var asyncOperationHandle = assetReference.InstantiateAsync(position, Quaternion.identity);
            await asyncOperationHandle.Task;
            return asyncOperationHandle.Result.GetComponent<T>();
        }

    }
}