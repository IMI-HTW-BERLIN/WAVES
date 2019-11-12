using Interfaces;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class CollisionType<T> : MonoBehaviour where T : Damageable
    {
        protected bool IgnoreOtherTriggerCollider;
        protected abstract void Collided(T other);

        protected abstract void TriggerCollided(T other);

        private void OnCollisionEnter2D(Collision2D other)
        {
            T collidedWithT = other.gameObject.GetComponent<T>();
            if (!collidedWithT) return;
            Collided(collidedWithT);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (IgnoreOtherTriggerCollider && other.isTrigger) return;
            T collidedWithT = other.GetComponent<T>();
            if (!collidedWithT) return;
            TriggerCollided(collidedWithT);
        }
    }
}