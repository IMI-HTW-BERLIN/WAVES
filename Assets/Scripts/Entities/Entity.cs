using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        private int _currentHealth;
        
        [SerializeField]
        protected int health;
        [SerializeField]
        protected int baseMovementSpeed;
        [SerializeField]
        protected int baseDamage;

        protected virtual void Start()
        {
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