using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DataAccess
{
    public interface UnitsDataAccess
    {
        IReadOnlyList<Unit> GetAllUnits();
        Task<IReadOnlyList<UserUnit>> AddUnitsToUser(List<UnitToAdd> units);
        Task RemoveUnitsToUser(List<UserUnit> unitsToRemove);
        IReadOnlyList<string> GetInitialUnitsId();
        IReadOnlyList<UserUnit> GetAllUserUnits();
        UserUnit GetUserUnit(string id);
    }
}