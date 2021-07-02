using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DataAccess
{
    public interface GameplayUnitsDataAccess
    {
        Task Add(UserUnit userUnit);
        IReadOnlyList<GameplayUnit> GetAllAllyUnits();
        IReadOnlyList<GameplayUnit> GetAllEnemyUnits();
    }
}