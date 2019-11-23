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
        public virtual void ApplyDamage(int damage) => UpdateHealthBar();

        public void AddMaxHealth(int health) => maxHealth += health;

        protected void UpdateHealthBar()
        {
            if (healthBar != null) healthBar.SetHealthBar(CurrentHealth, maxHealth);
        }
    }
}