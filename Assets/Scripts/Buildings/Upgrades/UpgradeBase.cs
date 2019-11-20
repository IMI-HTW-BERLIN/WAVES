using UnityEngine;

namespace Buildings.Upgrades
{
    [RequireComponent(typeof(Building))]
    public abstract class UpgradeBase : MonoBehaviour
    {
        protected Building Building;

        protected virtual void Awake()
        {
            Building = GetComponent<Building>();
        }

        private void OnEnable()
        {
            Building.OnUpgrade += Upgrade;
        }

        private void OnDisable()
        {
            Building.OnUpgrade -= Upgrade;
        }

        protected abstract void Upgrade(int level);
    }
}