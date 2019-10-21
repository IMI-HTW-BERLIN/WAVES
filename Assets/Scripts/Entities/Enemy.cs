using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField]
        private Transform target;

        private void FixedUpdate() => MoveToTarget();

        public void SetTarget(Transform targetTransform) => this.target = targetTransform;

        private void MoveToTarget()
        {
            Vector3 position = transform.position;
            bool targetToLeft = position.x > target.position.x;
            rigid.MovePosition(new Vector3(position.x + baseMovementSpeed * Time.fixedDeltaTime * (targetToLeft ? - 1 : 1), position.y, position.z));
        }
        
    }
}