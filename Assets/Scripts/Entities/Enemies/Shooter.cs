using Interfaces;
using UnityEngine;
using Weapons;

namespace Entities.Enemies
{
    public class Shooter : Enemy
    {
        [Header("Shooter")] [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletSpeed;

        protected override void Attack(Damageable damageable)
        {
            Vector3 position = transform.position;
            Vector2 direction = damageable.transform.position - position;
            Bullet bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.Shoot(typeof(Damageable), baseDamage, direction * bulletSpeed);
        }
    }
}