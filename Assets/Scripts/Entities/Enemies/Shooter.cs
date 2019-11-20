using Interfaces;
using UnityEngine;
using Utils;
using Weapons;

namespace Entities.Enemies
{
    public class Shooter : Enemy
    {
        [Header("Shooter")] [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private Range linearDragRange;

        private void OnEnable() => Rb.drag = linearDragRange.GetRandom();

        /// <summary>
        /// Shoots the target.
        /// </summary>
        /// <param name="damageable">The target to attack</param>
        protected override void Attack(Damageable damageable)
        {
            Vector3 position = transform.position;
            Vector2 direction = damageable.transform.position - position;
            Bullet bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.Shoot(baseDamage, direction * bulletSpeed);
        }
    }
}