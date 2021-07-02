using Domain.DataAccess;

namespace Domain.UseCases.Gameplay
{
    public class SpawnUnitUseCase : UnitSpawner
    {
        private readonly UnitsDataAccess _unitsDataAccess;
        private readonly GameplayUnitsDataAccess _gameplayUnitsDataAccess;

        public SpawnUnitUseCase(UnitsDataAccess unitsDataAccess, 
            GameplayUnitsDataAccess gameplayUnitsDataAccess)
        {
            _unitsDataAccess = unitsDataAccess;
            _gameplayUnitsDataAccess = gameplayUnitsDataAccess;
        }

        public async void Spawn(string id)
        {
            var userUnit = _unitsDataAccess.GetUserUnit(id);
            await _gameplayUnitsDataAccess.Add(userUnit);
        }
    }
}