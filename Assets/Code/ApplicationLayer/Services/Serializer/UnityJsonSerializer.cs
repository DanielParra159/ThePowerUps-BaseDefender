using Domain.Services.Serializer;
using UnityEngine;

namespace ApplicationLayer.Services.Serializer
{
    public class UnityJsonSerializer : SerializerService
    {
        public string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public T Deserialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}
