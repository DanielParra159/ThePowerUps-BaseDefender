using Code.SharedTypes.Units;

namespace Domain.DataAccess
{
    public class UnitToAdd
    {
        public const string InitialUnitsAnnotation = "Initial units";
        
        public readonly string Id;
        public readonly string Annotation;
        public readonly UnitState UnitState;

        public UnitToAdd(string id, string annotation, UnitState unitState)
        {
            Id = id;
            Annotation = annotation;
            UnitState = unitState;
        }
    }
}