using System;
using Code.SharedTypes.Units;
using UnityEngine;

namespace ApplicationLayer.Services.Server.Dtos.User
{
    public class InventoryItemDto : Dto
    {
        public readonly string ItemId;
        public readonly string InstanceId;
        private readonly object _customData;

        public InventoryItemDto(string itemId, string instanceId, object customData)
        {
            ItemId = itemId;
            InstanceId = instanceId;
            _customData = customData;
        }

        public T GetCustomData<T>()
        {
            return (T) _customData;
        }
    }
}