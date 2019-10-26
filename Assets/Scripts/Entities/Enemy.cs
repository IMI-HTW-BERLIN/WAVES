using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField]
        private Transform target;

        private void FixedUpdate() => MoveToTarget();

        public void SetTarget(Transform targetTransform) => this.target = targetTransform;

        private void MoveToTarget()
        {
            Vector3 position = transform.position;
            bool targetToLeft = position.x > target.position.x;
            rigidbody2D.MovePosition(new Vector3(position.x + baseMovementSpeed * Time.fixedDeltaTime * (targetToLeft ? - 1 : 1), position.y, position.z));
        }
        
    }
}