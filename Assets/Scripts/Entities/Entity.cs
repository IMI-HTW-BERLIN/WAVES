using Interfaces;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int health;
        [SerializeField] protected int baseMovementSpeed;
        [SerializeField] protected int baseDamage;
        public int BaseDamage => baseDamage;

        protected Rigidbody2D Rb;
        protected Collider2D Collider2D;

        private int _currentHealth;

        protected virtual void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
            Rb = GetComponent<Rigidbody2D>();
            _currentHealth = health;
        }

        /// <summary>
        /// Destroys the game object. Allows behaviour before death.
        /// </summary>
        protected virtual void OnDeath() => Destroy(this.gameObject);

        /// <inheritdoc />
        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth > 0) return;

            Debug.Log("Entity '" + name + "' died");
            OnDeath();
        }

        public void ResetHealth() => _currentHealth = health;
    }
}