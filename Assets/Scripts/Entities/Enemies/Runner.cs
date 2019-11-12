using Interfaces;
using UnityEngine;
using Utils;

namespace Entities.Enemies
{
    /// <summary>
    /// Fast and small enemy.
    /// </summary>
    public class Runner : Enemy
    {
        [SerializeField] private Range attackJumpForceX;
        [SerializeField] private Range attackJumpForceY;
        [SerializeField] private Range attackRepulseForce;


        protected override void Attack(Damageable damageable)
        {
            Rb.AddForce(new Vector2(
                attackJumpForceX.GetRandom() * (EnemyToLeft ? -1 : 1),
                attackJumpForceY.GetRandom())
            );
        }

        private void GetRepulsed() =>
            Rb.AddForce(new Vector2(attackRepulseForce.GetRandom() * (EnemyToLeft ? 1 : -1), 0f));

        private void OnCollisionEnter2D(Collision2D other)
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            damageable.ApplyDamage(baseDamage);
            GetRepulsed();
        }
    }
}