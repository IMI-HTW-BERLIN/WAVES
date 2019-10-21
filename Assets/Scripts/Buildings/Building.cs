using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        protected int health;
        [SerializeField]
        private GameObject destructionParticleEffect;
        [SerializeField]
        private int maxLevel;
        
        private int _level;

        private void Start()
        {
            _level = 1;
        }

        /// <summary>
        /// Applies the given amount of damage to the building
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        public void ApplyDamage(int damage)
        {
            health -= damage;
            if (health <= 0) Destroy();
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
        /// Called when a building is upgraded to the next level
        /// </summary>
        protected virtual void Upgrade()
        {
            if (_level >= maxLevel)
                return;
            Instantiate(destructionParticleEffect, transform);
            _level++;
        }
    }
}
