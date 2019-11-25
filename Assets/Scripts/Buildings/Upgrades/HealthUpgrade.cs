namespace Buildings.Upgrades
{
    public class HealthUpgrade : UpgradeBase
    {
        protected override void Upgrade(int level)
        {
            // If upgrade doesn't exist, return
            if (level > upgradeValues.Length) return;

            // Upgrade max health for level
            Building.AddMaxHealth(upgradeValues[level]);
        }
    }
}