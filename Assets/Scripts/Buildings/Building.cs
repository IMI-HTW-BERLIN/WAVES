using System;
using Interfaces;
using Managers;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class Building : Damageable
    {
        [Header("Building")] [SerializeField] private int repairCost;
        [SerializeField] private int[] upgradeLevelCosts;
        [SerializeField] private GameObject destructionParticleEffect;

        [NonSerialized] public int MaxLevel;

        private int _currentLevel;

        public delegate void Upgrade(int level);

        public event Upgrade OnUpgrade;

        /// <inheritdoc />
        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0) Destroy();
        }

        /// <summary>
        /// Called when a building's health drops to or below zero
        /// </summary>
        protected virtual void Destroy()
        {
            Instantiate(destructionParticleEffect, transform);
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// Checks if a building is on its maximum level
        /// </summary>
        /// <returns></returns>
        public bool IsMaxLevel() => _currentLevel >= MaxLevel;

        /// <summary>
        /// Checks if a building's health is on its maximum
        /// </summary>
        /// <returns></returns>
        public bool IsMaxHealth() => CurrentHealth == maxHealth;

        /// <summary>
        /// Called when a building is upgraded to the next level
        /// </summary>
        public void UpgradeBuilding()
        {
            if (IsMaxLevel()) return;
            //Check if player has enough money
            if (!ResourceManager.Instance.RemoveGold(upgradeLevelCosts[_currentLevel])) return;

            OnUpgrade?.Invoke(_currentLevel);
            Instantiate(destructionParticleEffect, transform);
            _currentLevel++;
        }

        /// <summary>
        /// Repairs a building by resetting its current health to the maximum
        /// </summary>
        public void Repair()
        {
            if (!ResourceManager.Instance.RemoveGold(repairCost)) return;
            CurrentHealth = maxHealth;
            UpdateHealthBar();
        }

        /// <summary>
        /// Sells a building
        /// </summary>
        public void Sell() => Destroy();
    }
}