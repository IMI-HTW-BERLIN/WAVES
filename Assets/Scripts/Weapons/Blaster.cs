using Entities.Enemies;
using UnityEngine;

namespace Weapons
{
    public class Blaster : Weapon
    {
        [SerializeField] private Transform firePosition;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private int damage;

        public override void Attack()
        {
            Transform weaponTransform = transform;
            Vector2 bulletSpawnPos = firePosition.position;
            Vector2 direction = weaponTransform.right;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
            bullet.Shoot(typeof(Enemy), damage, direction * bulletSpeed);
        }
    }
}