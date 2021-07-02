using System;
using UnityEngine;

namespace ApplicationLayer.Services.Server.Dtos.Server
{
    [Serializable]
    public class InitialUnitsDto : Dto
    {
        [SerializeField] private string[] unitsId;

        public string[] UnitsId => unitsId;
    }
}