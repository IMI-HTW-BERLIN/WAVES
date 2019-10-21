using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public int currentHealth;
        
        [SerializeField]
        protected int health;
        [SerializeField]
        protected int baseMovementSpeed;
        [SerializeField]
        protected int baseDamage;

        protected virtual void Start()
        {
            currentHealth = health;
        }

        /// <summary>
        /// Destroys the game object. Allows behaviour before death.
        /// </summary>
        protected virtual void OnDeath() => Destroy(this.gameObject);

        public void ReduceHealth(int amount)
        {
            currentHealth -= amount;
            if (currentHealth > 0) return;
            
            Debug.Log("Entity '" + this.name + "' died");
            OnDeath();
        }

        public void ResetHealth() => currentHealth = health;
    }
}