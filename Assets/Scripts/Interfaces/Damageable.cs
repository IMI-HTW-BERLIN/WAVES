using UI;
using UnityEngine;

namespace Interfaces
{
    public abstract class Damageable : MonoBehaviour
    {
        [Header("Damageable-Base")] [SerializeField]
        protected int maxHealth;

        [SerializeField] private HealthBar healthBar;

        protected int CurrentHealth;

        protected virtual void Awake() => CurrentHealth = maxHealth;

        /// <summary>
        /// Applies the given amount of damage to the object
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        public void ApplyDamage(int damage)
        {
            CurrentHealth -= damage;
            UpdateHealthBar();

            if (CurrentHealth <= 0)
                Destroy();
        }

        /// <summary>
        /// Called when the current health drops to or below zero
        /// </summary>
        protected abstract void Destroy();

        public void AddMaxHealth(int health) => maxHealth += health;

        protected void UpdateHealthBar()
        {
            if (healthBar != null) healthBar.SetHealthBar(CurrentHealth, maxHealth);
        }
    }
}