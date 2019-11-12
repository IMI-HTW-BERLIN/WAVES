using System;
using Entities.Enemies;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// A bullet with a specified target.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        // TODO Should be defined by the weapon
        [SerializeField] private float lifeTime;

        private Rigidbody2D _rb;
        private int _damage;

        private Type _enemyType;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifeTime);
        }

        public void Shoot(Type enemyType, int damage, Vector2 force)
        {
            _enemyType = enemyType;
            _damage = damage;
            _rb.AddForce(force);
        }

        /// <summary>
        /// Target hit? -> Do damage, then delete me
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Only listen for actual hits with other object-colliders
            if (other.isTrigger) return;
            Enemy enemy = other.GetComponent<Enemy>();
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