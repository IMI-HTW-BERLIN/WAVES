using System;
using Interfaces;
using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Enemy : Entity
    {
        [Header("Enemy-Base")] [SerializeField]
        private Transform target;

        [Tooltip("Defines the max speed by using the velocity of the rigidbody")] [SerializeField]
        private float maxSpeed;


        protected bool EnemyToLeft;

        private bool TargetToLeft => transform.position.x > target.position.x;
        private bool _move = true;

        /// <summary>
        /// Move to target if not attacking.
        /// </summary>
        protected void FixedUpdate()
        {
            if (target && _move)
                MoveToTarget();
        }

        /// <summary>
        /// Sets the target for the 
        /// </summary>
        /// <param name="targetTransform"></param>
        public void SetTarget(Transform targetTransform) => target = targetTransform;

        /// <summary>
        /// This method will automatically be called by <see cref="ExecuteAttack"/>.
        /// </summary>
        protected abstract void Attack(Damageable damageable);

        /// <summary>
        /// Moves to the target aka. the player-base.
        /// </summary>
        protected virtual void MoveToTarget()
        {
            if (Math.Abs(Rb.velocity.x) < maxSpeed)
                Rb.AddForce(new Vector2(baseMovementSpeed * Time.fixedDeltaTime * (TargetToLeft ? -1 : 1), 0));
        }

        /// <summary>
        /// Executes an attack when a damageable object is in reach.
        /// </summary>
        protected virtual void ExecuteAttack(Damageable damageable)
        {
            _move = false;
            FlipEntity(TargetToLeft);
            EnemyToLeft = transform.position.x > damageable.transform.position.x;
            if (CanAttack)
                Attack(damageable);
        }

        /// <summary>
        /// This defines the attack range of the enemy.
        /// </summary>
        /// <param name="other">The collider of the other object</param>
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!target) return;
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable)
                ExecuteAttack(damageable);
        }

        /// <summary>
        /// When exiting the damageable collider, continue to move to the target.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!target) return;
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (!damageable) return;

            FlipEntity(TargetToLeft);
            _move = true;
        }
    }
}