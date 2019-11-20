namespace Buildings.Upgrades
{
    public class HealthUpgrade : UpgradeBase
    {
        protected override void Awake()
        {
            base.Awake();
            if (Building.MaxLevel < upgrades.Length)
                Building.MaxLevel = upgrades.Length;
        }

        protected override void Upgrade(int level)
        {
            // If upgrade doesn't exist, return
            if (level > upgrades.Length) return;

            // Upgrade max health for level
            Building.AddMaxHealth(upgrades[level].valueIncrease);
        }
    }
}