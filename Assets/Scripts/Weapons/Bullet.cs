using Entities;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private int _damage;
        
        public void Initialize(int damage, Vector3 force)
        {
            _damage = damage;
            _rb.AddForce(force);
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 3);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy;
            if ((enemy = other.GetComponent<Enemy>()) != null)
                enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}
