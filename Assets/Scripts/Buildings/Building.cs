﻿using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class Building : MonoBehaviour
    {
        [SerializeField] protected int maxHealth;

        [SerializeField] private GameObject destructionParticleEffect;
        [SerializeField] private int maxLevel;
        [SerializeField] private HealthBarBuilding healthBar;

        private int _currentHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxLevel => maxLevel;
        public int Level { get; private set; }

        private void Start()
        {
            Level = 1;
            _currentHealth = maxHealth;
        }

        /// <summary>
        /// Applies the given amount of damage to the building
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            healthBar.SetHealthBar(_currentHealth, maxHealth);
            if (_currentHealth <= 0) Destroy();
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
        public bool IsMaxLevel() => Level >= maxLevel;

        /// <summary>
        /// Called when a building is upgraded to the next level
        /// </summary>
        protected virtual void Upgrade()
        {
            if (IsMaxLevel())
                return;
            Instantiate(destructionParticleEffect, transform);
            Level++;
        }
    }
}