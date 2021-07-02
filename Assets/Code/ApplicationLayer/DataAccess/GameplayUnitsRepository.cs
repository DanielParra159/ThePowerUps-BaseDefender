using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayer.Services.Instantiators;
using Domain.DataAccess;
using Domain.Entities;
using UnityEngine;

namespace ApplicationLayer.DataAccess
{
    public class GameplayUnitsRepository : GameplayUnitsDataAccess
    {
        private readonly UnitInstantiatorGateway _unitInstantiatorGateway;
        private readonly List<GameplayUnit> _allyGameplayUnits;
        private readonly List<GameplayUnit> _enemyGameplayUnits;

        public GameplayUnitsRepository(UnitInstantiatorGateway unitInstantiatorGateway)
        {
            _unitInstantiatorGateway = unitInstantiatorGateway;
            _allyGameplayUnits = new List<GameplayUnit>();
            _enemyGameplayUnits = new List<GameplayUnit>();
        }

        public async Task Add(UserUnit userUnit)
        {
            var spawnPosition = new Vector3(0, 0, 0); // Get base position
            var unitConfiguration = new UnitConfiguration(userUnit.UnitId, spawnPosition);

            var instanceId = await _unitInstantiatorGateway.Instantiate(unitConfiguration);
            var gameplayUnit = new GameplayUnit(userUnit, instanceId, spawnPosition.x, 1);
            _allyGameplayUnits.Add(gameplayUnit);
            _allyGameplayUnits.Sort((unit1, unit2) => unit1.XPosition > unit2.XPosition ? 1 : -1);
        }

        public IReadOnlyList<GameplayUnit> GetAllAllyUnits()
        {
            return _allyGameplayUnits;
        }

        public IReadOnlyList<GameplayUnit> GetAllEnemyUnits()
        {
            return _enemyGameplayUnits;
        }
    }
}