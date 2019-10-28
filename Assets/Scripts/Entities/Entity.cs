using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Collider2D))]
    public class Entity : MonoBehaviour
    {
        private int _currentHealth;
        protected Collider2D Collider2D;
        
        [SerializeField] protected int health;
        [SerializeField] protected int baseMovementSpeed;
        [SerializeField] protected int baseDamage;
        [SerializeField] protected Rigidbody2D rb;

        public int BaseDamage => baseDamage;

        private void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
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