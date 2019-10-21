using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        [SerializeField] private Rigidbody2D rigid;
        private Vector3 _target;

        private void FixedUpdate() => MoveToTarget();

        public void SetTarget(Vector2 target) => _target = target;

        private void MoveToTarget()
        {
            Vector3 position = transform.position;
            Vector3 direction = (_target - position).normalized;
            rigid.MovePosition(position + Time.deltaTime * baseMovementSpeed * direction);
        }
        
    }
}