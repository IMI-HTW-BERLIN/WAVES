using UnityEngine;

namespace Buildings.Upgrades
{
    [RequireComponent(typeof(GunTower))]
    public class RangeUpgrade : UpgradeBase<float>
    {
        protected override void Upgrade(int level)
        {
            // If upgrade doesn't exist, return
            if (level >= upgradeValues.Length) return;
            
            // Upgrade attack damage
            (Building as GunTower)?.IncreaseAttackRange(upgradeValues[level]);
        }
    }
}