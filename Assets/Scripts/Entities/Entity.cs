using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Entity : MonoBehaviour
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

        public void ReduceHealth(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth > 0) return;

            Debug.Log("Entity '" + this.name + "' died");
            OnDeath();
        }

        public void ResetHealth() => _currentHealth = health;
    }
}