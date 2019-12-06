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
        [SerializeField] private float bulletSpeed;
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
        /// <param name="direction"></param>
        public void Shoot(int damage, Vector2 direction)
        {
            _damage = damage;
            _rb.AddForce(direction * bulletSpeed);
        }

        /// <summary>
        /// Deal damage and destroy itself
        /// </summary>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (attackableLayers.Contains(other.gameObject.layer))
            {
                if (other.gameObject.TryGetComponent(out Damageable enemy))
                    enemy.ApplyDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}