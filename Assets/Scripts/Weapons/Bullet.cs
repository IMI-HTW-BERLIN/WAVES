using Entities;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;

        public void SetDamage(int damage) => _damage = damage;

        private void Start() => Destroy(gameObject, 3);

        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy;
            if ((enemy = other.GetComponent<Enemy>()) != null)
                enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}
