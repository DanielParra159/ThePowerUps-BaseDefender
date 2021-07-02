using UnityEngine;

namespace ApplicationLayer.DataAccess
{
    public class UnitConfiguration
    {
        public readonly string Id;
        public readonly Vector3 Position;

        public UnitConfiguration(string id, Vector3 position)
        {
            Id = id;
            Position = position;
        }
    }
}