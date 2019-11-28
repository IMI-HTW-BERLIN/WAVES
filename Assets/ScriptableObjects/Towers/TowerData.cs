using Buildings;
using UnityEngine;

namespace ScriptableObjects.Towers
{
    [CreateAssetMenu(fileName = "Tower_1", menuName = "ScriptableObjects/Towers/TowerData", order = 0)]
    public class TowerData : ScriptableObject
    {
        public int cost;
        public Building prefab;
        public Blueprint blueprintPrefab;
        public Sprite buildMenuIcon;
    }
}