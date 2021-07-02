using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace SystemUtilities
{
    public static class TaskExtensions
    {
        public static async void WrapErrors(this Task task)
        {
            await task;
        }

        public static IEnumerator AsCoroutine(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public static Task FromCoroutine(this AsyncOperation asyncOperation)
        {
            return Task.Run(async () =>
                            {
                                while (!asyncOperation.isDone)
                                {
                                    await Task.Yield();
                                }
                            });
        }
        
    }
}
