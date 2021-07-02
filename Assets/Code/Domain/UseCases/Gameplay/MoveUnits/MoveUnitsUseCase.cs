using System;
using System.Collections.Generic;
using Domain.DataAccess;
using Domain.Entities;
using Domain.Services.EventDispatcher;

namespace Domain.UseCases.Gameplay.MoveUnits
{
    public class MoveUnitsUseCase
    {
        private readonly GameplayUnitsDataAccess _gameplayUnitsDataAccess;
        private readonly EventDispatcherService _eventDispatcherService;

        public MoveUnitsUseCase(GameplayUnitsDataAccess gameplayUnitsDataAccess,
            EventDispatcherService eventDispatcherService)
        {
            _gameplayUnitsDataAccess = gameplayUnitsDataAccess;
            _eventDispatcherService = eventDispatcherService;
        }

        public void Move(float deltaTime)
        {
            var enemyUnits = _gameplayUnitsDataAccess.GetAllEnemyUnits();
            var allyUnits = _gameplayUnitsDataAccess.GetAllAllyUnits();
            var unitsData = new List<UnitsMoved.UnitData>();

            MoveUnits(deltaTime, allyUnits, ref unitsData);
            MoveUnits(deltaTime, enemyUnits, ref unitsData);

            _eventDispatcherService.Dispatch(new UnitsMoved(unitsData));
        }

        private static void MoveUnits(float deltaTime, IReadOnlyList<GameplayUnit> allyUnits,
            ref List<UnitsMoved.UnitData> unitsData)
        {
            foreach (var allAllyUnit in allyUnits)
            {
                if (!allAllyUnit.CanMove())
                {
                    continue;
                }

                allAllyUnit.MoveXPosition(deltaTime);
                unitsData.Add(new UnitsMoved.UnitData(allAllyUnit.InstanceId, allAllyUnit.XPosition));
            }
        }

        
    }
}