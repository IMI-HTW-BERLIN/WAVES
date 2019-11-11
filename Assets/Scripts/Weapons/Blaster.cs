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
            Vector3 bulletSpawnPos = firePosition.position;
            Vector2 direction = transform.right;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
            bullet.SetDamage(damage);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        }
    }
}