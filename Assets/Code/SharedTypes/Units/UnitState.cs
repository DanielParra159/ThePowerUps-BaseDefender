using System;
using UnityEngine;

namespace Code.SharedTypes.Units
{
    [Serializable]
    public class UnitState
    {
        [SerializeField] private int level;

        public UnitState(int level)
        {
            this.level = level;
        }
    }
}