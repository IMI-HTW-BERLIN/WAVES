using Managers;
using UnityEngine;

namespace ScriptableObjects.Waves
{
    [CreateAssetMenu(fileName = "Wave_1", menuName = "ScriptableObjects/Waves/WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public WaveManager.SpawnData[] spawnData;
    }
}