using System;
using UnityEngine;

namespace Code.SharedTypes.Units
{
    [Serializable]
    public class UnitAttributes
    {
        [SerializeField] private int health;
        [SerializeField] private int healthIncrementPerLevel;
        [SerializeField] private int attack;
        [SerializeField] private int attackIncrementPerLevel;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float secondsBetweenAttacks;
        [SerializeField] private int initialUpgradeCost;
        [SerializeField] private int invocationSecondsCooldown;
        [SerializeField] private int attackRange;

        public int Health => health;
        public int HealthIncrementPerLevel => healthIncrementPerLevel;
        public int Attack => attack;
        public int AttackIncrementPerLevel => attackIncrementPerLevel;
        public float MovementSpeed => movementSpeed;
        public float SecondsBetweenAttacks => secondsBetweenAttacks;
        public int InitialUpgradeCost => initialUpgradeCost;
        public int InvocationSecondsCooldown => invocationSecondsCooldown;
        public float AttackRange => attackRange;

        public UnitAttributes(int health, int healthIncrementPerLevel, int attack, int attackIncrementPerLevel,
            int movementSpeed, int secondsBetweenAttacks, int initialUpgradeCost, int invocationSecondsCooldown)
        {
            this.health = health;
            this.healthIncrementPerLevel = healthIncrementPerLevel;
            this.attack = attack;
            this.attackIncrementPerLevel = attackIncrementPerLevel;
            this.movementSpeed = movementSpeed;
            this.secondsBetweenAttacks = secondsBetweenAttacks;
            this.initialUpgradeCost = initialUpgradeCost;
            this.invocationSecondsCooldown = invocationSecondsCooldown;
        }
    }
}