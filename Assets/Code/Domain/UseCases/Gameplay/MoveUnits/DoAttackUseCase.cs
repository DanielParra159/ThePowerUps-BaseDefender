using System.Collections.Generic;
using Domain.DataAccess;
using Domain.Entities;

namespace Domain.UseCases.Gameplay.MoveUnits
{
    public class DoAttackUseCase
    {
        private readonly GameplayUnitsDataAccess _gameplayUnitsDataAccess;

        public DoAttackUseCase(GameplayUnitsDataAccess gameplayUnitsDataAccess)
        {
            _gameplayUnitsDataAccess = gameplayUnitsDataAccess;
        }
        
        public void TryAttack()
        {
            var enemyUnits = _gameplayUnitsDataAccess.GetAllEnemyUnits();
            var allyUnits = _gameplayUnitsDataAccess.GetAllAllyUnits();

            TryAttack(allyUnits, enemyUnits[0]);
            TryAttack(enemyUnits, allyUnits[allyUnits.Count - 1]);
        }

        private static void TryAttack(IReadOnlyList<GameplayUnit> allyUnits, GameplayUnit nearestEnemy)
        {
            foreach (var allAllyUnit in allyUnits)
            {
                if (allAllyUnit.CanAttack(nearestEnemy))
                {
                    allAllyUnit.Attack();
                    var isTheEnemyDeath = nearestEnemy.ReceiveDamage(allAllyUnit);
                    // Dispatch event
                }
            }
        }
    }
}