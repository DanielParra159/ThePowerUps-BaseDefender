using System;
using UnityEngine;

namespace ApplicationLayer.Services.Server.Dtos.User
{
    [Serializable]
    public class IsInitializedDto : Dto
    {
        [SerializeField] private bool _isInitialized;
        public bool IsInitialized => _isInitialized;

        public IsInitializedDto(bool isInitialized)
        {
            _isInitialized = isInitialized;
        }
    }
}