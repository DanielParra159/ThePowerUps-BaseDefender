using System.Collections.Generic;
using Domain.Services.EventDispatcher;

namespace Domain.UseCases.Gameplay.MoveUnits
{
    public class UnitsMoved : ISignal
    {
        public class UnitData
        {
            public readonly int InstanceId;
            public readonly float XPosition;

            public UnitData(int instanceId, float xPosition)
            {
                InstanceId = instanceId;
                XPosition = xPosition;
            }
        }

        public readonly IReadOnlyList<UnitData> UnitsData;

        public UnitsMoved(IReadOnlyList<UnitData> unitsData)
        {
            UnitsData = unitsData;
        }

        public string Print()
        {
            return $"{nameof(UnitsData)}: {UnitsData}";
        }
    }
}