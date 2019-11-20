using System;
using Managers;
using UnityEngine;

namespace Buildings.Upgrades
{
    [RequireComponent(typeof(Building))]
    public abstract class UpgradeBase : MonoBehaviour
    {
        [SerializeField] protected UpgradeData[] upgrades;

        protected Building Building;

        protected virtual void Awake() => Building = GetComponent<Building>();

        private void OnEnable() => Building.OnUpgrade += BuyUpgrade;

        private void OnDisable() => Building.OnUpgrade -= BuyUpgrade;

        protected abstract void Upgrade(int level);

        private void BuyUpgrade(int level)
        {
            if (!ResourceManager.Instance.RemoveGold(upgrades[level].goldCost)) return;
            Upgrade(level);
        }


        [Serializable]
        public struct UpgradeData
        {
            public int valueIncrease;
            public int goldCost;
        }
    }
}