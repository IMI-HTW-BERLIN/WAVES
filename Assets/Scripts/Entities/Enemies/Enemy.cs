using System;
using System.Collections.Generic;
using Cinemachine;
using Interfaces;
using Managers;
using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Enemy : Entity
    {
        [Header("Enemy-Base")]
        [Tooltip("Defines the max speed by using the velocity of the rigidbody")]
        [SerializeField]
        private float maxSpeed;

        [Tooltip("Defines how much the player earns for killing this enemy")] [SerializeField]
        private int goldValue;

        [SerializeField] private ParticleSystem enemyDeathPS;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        protected bool EnemyToLeft;

        private bool TargetToLeft => transform.position.x > _playerBase.position.x;
        private Transform _playerBase;
        private bool _move = true;
        private bool _attacking;
        private readonly List<Damageable> _currentTargets = new List<Damageable>();

        /// <summary>
        /// Continues to attack the closest target, if there is one
        /// </summary>
        private void Update()
        {
            if (_attacking && CanAttack && _currentTargets.Count > 0)
            {
                Damageable closest = _currentTargets[0];
                for (int i = 1; i < _currentTargets.Count; i++)
                {
                    Damageable damageable = _currentTargets[i];
                    if (Vector3.Distance(damageable.transform.position, transform.position) <
                        Vector3.Distance(closest.transform.position, transform.position))
                        closest = damageable;
                }

                Attack(closest);
            }
        }

        /// <summary>
        /// Move to target if not attacking.
        /// </summary>
        protected void FixedUpdate()
        {
            if (_playerBase && _move)
                MoveToTarget();
        }

        /// <summary>
        /// Sets the target for the 
        /// </summary>
        /// <param name="targetTransform"></param>
        public void SetPlayerBase(Transform targetTransform) => _playerBase = targetTransform;

        /// <summary>
        /// This method will automatically be called each frame
        /// </summary>
        protected abstract void Attack(Damageable damageable);

        protected override void OnDeath()
        {
            ResourceManager.Instance.AddGold(goldValue);
            GameManager.Instance.IncreaseScore();
            base.OnDeath();
            Instantiate(enemyDeathPS, this.transform.position, Quaternion.identity);
            impulseSource.GenerateImpulse();
        }

        /// <summary>
        /// Moves to the target aka. the player-base.
        /// </summary>
        private void MoveToTarget()
        {
            if (Math.Abs(Rb.velocity.x) < maxSpeed)
                Rb.AddForce(new Vector2(baseMovementSpeed * Time.fixedDeltaTime * (TargetToLeft ? -1 : 1), 0));
        }

        /// <summary>
        /// Adds a target to the list and triggers attack mode.
        /// </summary>
        private void AddTarget(Damageable damageable)
        {
            _currentTargets.Add(damageable);
            _move = false;
            _attacking = true;
            EnemyToLeft = transform.position.x > damageable.transform.position.x;
            FlipEntity(TargetToLeft);
        }

        /// <summary>
        /// Removes a target from the target list (when destroyed or out of range)
        /// </summary>
        /// <param name="damageable">The damageable that is not there anymore</param>
        private void RemoveTarget(Damageable damageable)
        {
            _currentTargets.Remove(damageable);
            if (_currentTargets.Count == 0)
            {
                _attacking = false;
                FlipEntity(TargetToLeft);
                _move = true;
            }
        }

        /// <summary>
        /// This defines the attack range of the enemy.
        /// </summary>
        /// <param name="other">The collider of the other object</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_playerBase) return;
            if (other.TryGetComponent(out Damageable damageable))
                AddTarget(damageable);
        }

        /// <summary>
        /// When exiting the damageable collider, continue to move to the target.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_playerBase) return;
            if (other.TryGetComponent(out Damageable damageable))
                RemoveTarget(damageable);
        }
    }
}