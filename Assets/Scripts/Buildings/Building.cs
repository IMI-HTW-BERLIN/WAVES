using Interfaces;
using UI;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class Building : Damageable
    {
        [SerializeField] private GameObject destructionParticleEffect;
        [SerializeField] private int maxLevel;

        public int Level { get; private set; }

        private void Start()
        {
            Level = 1;
            CurrentHealth = maxHealth;
        }

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
        public bool IsMaxLevel() => Level >= maxLevel;

        /// <summary>
        /// Checks if a building's health is on its maximum
        /// </summary>
        /// <returns></returns>
        public bool IsMaxHealth() => CurrentHealth == maxHealth;

        /// <summary>
        /// Called when a building is upgraded to the next level
        /// </summary>
        public virtual void Upgrade()
        {
            if (IsMaxLevel())
                return;
            Instantiate(destructionParticleEffect, transform);
            Level++;
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