using Code.SharedTypes.Units;

namespace Domain.Entities
{
    public class Unit
    {
        public readonly string Id;
        public readonly string Name;
        public readonly UnitAttributes Attributes;

        public Unit(string id, string name, UnitAttributes attributes)
        {
            Id = id;
            Name = name;
            Attributes = attributes;
        }
    }
}