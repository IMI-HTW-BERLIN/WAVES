using System;
using System.Collections;
using Entities.Enemies;
using ScriptableObjects.Waves;
using UI;
using UnityEngine;
using World;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private WaveDisplay waveDisplay;
        [SerializeField] private WaveData[] waves;
        [SerializeField] private Transform[] enemySpawnPoints;

        private int _currentWaveIndex;

        private void Start()
        {
            Sun.SunDown += SpawnWave;
        }

        public void SpawnWave()
        {
            waveDisplay.ShowCurrentWave(_currentWaveIndex, 1f, 0.25f);
            WaveData wave = waves[_currentWaveIndex];
            //Using the data from SpawnData, spawns the enemies of this wave
            foreach (SpawnData spawnData in wave.spawnData)
                StartCoroutine(SpawnEnemies(spawnData));
            _currentWaveIndex++;

            //Spawns enemies defined in the SpawnData
            IEnumerator SpawnEnemies(SpawnData spawnData)
            {
                for (int i = 0; i < spawnData.numberOfSpawns; i++)
                {
                    //Left
                    Transform spawnPosition = enemySpawnPoints[0];
                    //Both
                    if (spawnData.spawnLocation == 1)
                        spawnPosition = enemySpawnPoints[i % enemySpawnPoints.Length];
                    //Right
                    else if (spawnData.spawnLocation == 2)
                        spawnPosition = enemySpawnPoints[1];

                    Enemy newEnemy = Instantiate(spawnData.enemy, spawnPosition.position, Quaternion.identity,
                        this.transform);

                    newEnemy.SetPlayerBase(GameManager.Instance.PlayerBase.transform);
                    yield return new WaitForSeconds(spawnData.spawnDelayPerUnit);
                }
            }
        }

        /// <summary>
        /// Each SpawnData represents an enemy-type, the number of enemies, the spawn location and spawn delay.
        /// A wave can have more than one SpawnData
        /// </summary>
        [Serializable]
        public struct SpawnData
        {
            public Enemy enemy;
            public int numberOfSpawns;

            [Tooltip("0-Left, 1-Both, 2-Right")] [Range(0, 2)]
            public int spawnLocation;

            public float spawnDelayPerUnit;
        }
    }
}