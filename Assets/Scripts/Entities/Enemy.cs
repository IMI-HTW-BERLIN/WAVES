using System;
using Buildings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 attackJumpForceY;
        [SerializeField] private Vector2 attackJumpForceX;
        [SerializeField] private Vector2 attackRepulseForce;
        [SerializeField] private float maxVelocity;

        private void FixedUpdate() => MoveToTarget();

        public void SetTarget(Transform targetTransform) => this.target = targetTransform;

        public void Attack()
        {
            rb.AddForce(new Vector2(
                Random.Range(attackJumpForceX.x, attackJumpForceX.y), 
                Random.Range(attackJumpForceY.x, attackJumpForceY.y))
            );
        }

        private void MoveToTarget()
        {
            Vector3 position = transform.position;
            bool targetToLeft = position.x > target.position.x;
            Debug.Log("Current velocity: " + rb.velocity.x);
            if (Math.Abs(rb.velocity.x) < maxVelocity)
                rb.AddForce(new Vector2(baseMovementSpeed * Time.fixedDeltaTime * (targetToLeft ? - 1 : 1), 0));
            
        }

        private void GetRepulsed()
        {
            rb.AddForce(new Vector2(Random.Range(attackRepulseForce.x, attackRepulseForce.y),0f));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Building building = other.gameObject.GetComponent<Building>();
            if(building == null) return;
            building.ApplyDamage(baseDamage);
            GetRepulsed();
        }
    }
}