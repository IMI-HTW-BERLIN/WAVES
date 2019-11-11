using System;
using Interfaces;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxVelocity;
        [SerializeField] private Range attackJumpForceX;
        [SerializeField] private Range attackJumpForceY;
        [SerializeField] private Range attackRepulseForce;

        private bool TargetToLeft => transform.position.x > target.position.x;
        private float _cooldownTime;

        private void FixedUpdate()
        {
            if (target) MoveToTarget();
        }

        public void SetTarget(Transform targetTransform) => target = targetTransform;

        public void Attack()
        {
            float time = Time.time;
            if (_cooldownTime + 1f / baseAttackSpeed > time)
                return;
            _cooldownTime = time;
            Rb.AddForce(new Vector2(
                attackJumpForceX.GetRandom() * (TargetToLeft ? -1 : 1),
                attackJumpForceY.GetRandom())
            );
        }

        private void MoveToTarget()
        {
            if (Math.Abs(Rb.velocity.x) < maxVelocity)
                Rb.AddForce(new Vector2(baseMovementSpeed * Time.fixedDeltaTime * (TargetToLeft ? -1 : 1), 0));
        }

        private void GetRepulsed() =>
            Rb.AddForce(new Vector2(attackRepulseForce.GetRandom() * (TargetToLeft ? 1 : -1), 0f));

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!target) return;
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            damageable.ApplyDamage(baseDamage);
            GetRepulsed();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!target) return;
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            Attack();
        }
    }
}