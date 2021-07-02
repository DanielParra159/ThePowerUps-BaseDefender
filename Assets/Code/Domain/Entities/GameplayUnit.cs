using UnityEngine;

namespace Domain.Entities
{
    public class GameplayUnit
    {
        private readonly UserUnit _userUnit;
        public readonly int InstanceId;
        public float XPosition { get; private set; }
        public int CurrentHealth { get; private set; }

        private readonly int _direction;
        private float _lastAttackTime;

        public GameplayUnit(UserUnit userUnit, int instanceId, float xPosition, int direction)
        {
            _userUnit = userUnit;
            InstanceId = instanceId;
            XPosition = xPosition;
            _direction = direction;
            CurrentHealth = _userUnit.UnitAttributes.Health;
            _lastAttackTime = 0;
        }

        public float MoveXPosition(float deltaTime)
        {
            XPosition += _userUnit.UnitAttributes.MovementSpeed * deltaTime * _direction;
            return XPosition;
        }

        public bool CanMove()
        {
            return !IsAttacking();
        }

        public bool CanAttack(GameplayUnit enemy)
        {
            if (IsAttacking())
            {
                return false;
            }

            var distanceBetweenUnits = Mathf.Abs(enemy.XPosition - XPosition);
            var isTheEnemyInRange = distanceBetweenUnits <= _userUnit.UnitAttributes.AttackRange;
            return isTheEnemyInRange;
        }

        private bool IsAttacking()
        {
            return _lastAttackTime + _userUnit.UnitAttributes.SecondsBetweenAttacks > Time.time;
        }

        public void Attack()
        {
            _lastAttackTime = Time.time;
        }

        public bool ReceiveDamage(GameplayUnit enemy)
        {
            CurrentHealth -= enemy._userUnit.UnitAttributes.Attack;
            return CurrentHealth <= 0;
        }
    }
}