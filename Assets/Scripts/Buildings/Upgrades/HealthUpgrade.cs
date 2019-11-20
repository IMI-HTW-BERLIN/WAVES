using UnityEngine;

namespace Buildings.Upgrades
{
    public class HealthUpgrade : UpgradeBase
    {
        [SerializeField] private int[] health;

        protected override void Awake()
        {
            base.Awake();
            if (Building.MaxLevel < health.Length)
                Building.MaxLevel = health.Length;
        }

        protected override void Upgrade(int level)
        {
            // If upgrade doesn't exist, return
            if (level > health.Length) return;

            // Upgrade max health for level
            Building.AddMaxHealth(health[level]);
        }
    }
}