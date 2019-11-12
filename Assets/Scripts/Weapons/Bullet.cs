using Entities.Enemies;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private int _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 3);
        }

        public void Shoot(int damage, Vector3 force)
        {
            _damage = damage;
            _rb.AddForce(force);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
                enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}