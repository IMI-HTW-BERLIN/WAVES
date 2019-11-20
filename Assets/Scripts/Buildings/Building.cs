using System;
using Interfaces;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class Building : Damageable
    {
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
            if (OnUpgrade == null || IsMaxLevel()) return;
            OnUpgrade.Invoke(_currentLevel);
            Instantiate(destructionParticleEffect, transform);
            _currentLevel++;
        }

        /// <summary>
        /// Repairs a building by resetting its current health to the maximum
        /// </summary>
        public void Repair()
        {
            CurrentHealth = maxHealth;
            UpdateHealthBar();
        }

        /// <summary>
        /// Sells a building
        /// </summary>
        public void Sell() => Destroy();
    }
}