using System;
using UnityEngine;

namespace Code.SharedTypes.Units
{
    [Serializable]
    public class UserUnitData
    {
        [SerializeField] private string itemId;
        [SerializeField] private string instanceId;
        [SerializeField] private UnitState unitState;
    }
}