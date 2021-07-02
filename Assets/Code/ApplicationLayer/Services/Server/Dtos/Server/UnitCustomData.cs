using System;
using Code.SharedTypes.Units;
using UnityEngine;

namespace ApplicationLayer.Services.Server.Dtos.Server
{
    [Serializable]
    public class UnitCustomData
    {
        [SerializeField] private UnitAttributes attributes;

        public UnitAttributes Attributes => attributes;
    }
}