using UnityEngine;

namespace Buildings.Upgrades
{
    [RequireComponent(typeof(Building))]
    public abstract class UpgradeBase<T> : MonoBehaviour
    {
        [SerializeField] protected T[] upgradeValues;

        protected Building Building;

        protected virtual void Awake()
        {
            Building = GetComponent<Building>();
            if (Building.MaxLevel < upgradeValues.Length)
                Building.MaxLevel = upgradeValues.Length;
        }

        private void OnEnable() => Building.OnUpgrade += Upgrade;

        private void OnDisable() => Building.OnUpgrade -= Upgrade;

        protected abstract void Upgrade(int level);
    }
}