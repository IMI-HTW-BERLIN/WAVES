namespace Interfaces
{
    public interface IDamageable
    {
        /// <summary>
        /// Applies the given amount of damage to the object
        /// </summary>
        /// <param name="damage">The amount of damage to apply</param>
        void ApplyDamage(int damage);
    }
}