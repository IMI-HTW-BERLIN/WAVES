using Interfaces;
using UnityEngine;
using Utils;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private LayerMask attackableLayers;

        private Rigidbody2D _rb;
        private int _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifeTime);
        }

        /// <summary>
        /// Shoots the bullet with a specified damage in a specified direction (and speed)
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="directionAndForce"></param>
        public void Shoot(int damage, Vector2 directionAndForce)
        {
            _damage = damage;
            _rb.AddForce(directionAndForce);
        }

        /// <summary>
        /// Target hit? -> Do damage, then delete me
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerMaskUtil.HasLayer(attackableLayers, other.gameObject.layer))
                return;

            //Only listen for actual hits with other object-colliders
            if (other.isTrigger) return;
            Damageable enemy = other.GetComponent<Damageable>();
            //If not an enemy, keep living
            if (!enemy) return;
            enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }

        /// <summary>
        /// Anything hit? -> Delete me
        /// </summary>
        private void OnCollisionEnter2D(Collision2D other) => Destroy(gameObject);
    }
}