using Interfaces;
using UnityEngine;

namespace Entities
{
    /// <summary>
    /// An Entity is an instance that can attack, be attacked, can move and die
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : Damageable
    {
        [Header("Entity-Base")] [SerializeField]
        protected int baseMovementSpeed;

        [SerializeField] protected int baseDamage;
        [SerializeField] protected float baseAttackSpeed;
        [SerializeField] public SpriteRenderer spriteRenderer;
        public int BaseDamage => baseDamage;

        protected Rigidbody2D Rb;

        private float _cooldownTime;

        /// <summary>
        /// Whether the entity can attack. Checks if the cooldown-time is over.
        /// </summary>
        protected bool CanAttack
        {
            get
            {
                float time = Time.time;
                if (1f / baseAttackSpeed + _cooldownTime > time)
                    return false;
                _cooldownTime = time;
                return true;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// <para>All sprites should FACE RIGHT.</para>
        /// Flips the sprite to the given direction.
        /// Override to add other things that need to be flipped/changed.
        /// </summary>
        /// <param name="facingLeft">Should it face left?</param>
        protected virtual void FlipEntity(bool facingLeft) => spriteRenderer.flipX = facingLeft;

        /// <summary>
        /// Destroys the game object. Allows behaviour before death.
        /// </summary>
        protected virtual void OnDeath() => Destroy(this.gameObject);

        /// <inheritdoc />
        /// TODO Should be in Damageable
        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            CurrentHealth -= damage;
            if (CurrentHealth > 0) return;

            OnDeath();
        }

        public void ResetHealth() => CurrentHealth = maxHealth;
    }
}