using Code.SharedTypes.Units;

namespace Domain.Entities
{
    public class UserUnit
    {
        private readonly Unit _unit;
        public string UnitId => _unit.Id;

        public readonly string InstanceId;
        private readonly UnitState _unitState;
        public UnitAttributes UnitAttributes => _unit.Attributes;

        public UserUnit(Unit unit, string instanceId, UnitState unitState)
        {
            _unit = unit;
            InstanceId = instanceId;
            _unitState = unitState;
        }

    }
}