using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Utils;
using Weapons;

namespace Buildings
{
    [RequireComponent(typeof(Collider2D))]
    public class GunTower : Building
    {
        [Header("GunTower")]
        [SerializeField] private GameObject barrel;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float attackSpeed;
        [SerializeField] private int attackDamage;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private LayerMask attackableLayers;
        [SerializeField] private CircleCollider2D attackRange;

        private List<Damageable> _targets = new List<Damageable>();
        
        private float _cooldownTime;

        private bool CanAttack
        {
            get
            {
                float time = Time.time;
                if (1f / attackSpeed + _cooldownTime > time)
                    return false;
                _cooldownTime = time;
                return true;
            }
        }
        
        private void Update()
        {
            if (CanAttack && GetNearestTarget() is Damageable closestTarget)
                Attack(closestTarget);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (!damageable || !attackableLayers.Contains(other.gameObject.layer)) return;
            _targets.Add(damageable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (!damageable) return;
            _targets.Remove(damageable);
        }

        public void IncreaseAttackDamage(int damage) => attackDamage += damage;
        public void IncreaseAttackRange(float range) => attackRange.radius += range;

        private void Attack(Damageable target)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 bulletSpawnPosition = this.bulletSpawnPoint.position;
            // Aim barrel towards nearest enemy
            barrel.transform.right = targetPosition - barrel.transform.position;
            // Fire bullet towards enemy
            Vector2 direction = targetPosition - bulletSpawnPosition;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
            bullet.Shoot(attackDamage, direction);
        }

        private Damageable GetNearestTarget()
        {
            if (_targets.Count <= 0) return null;
            
            Damageable closest = _targets[0];
            
            for (int i = 1; i < _targets.Count; i++)
            {
                Damageable damageable = _targets[i];
                if (Vector3.Distance(damageable.transform.position, transform.position) <
                    Vector3.Distance(closest.transform.position, transform.position))
                    closest = damageable;
            }

            return closest;
        }
    }
}