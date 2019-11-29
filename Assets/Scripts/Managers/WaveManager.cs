using System;
using System.Collections;
using Entities.Enemies;
using ScriptableObjects.Waves;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using World;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private WaveDisplay waveDisplay;
        [SerializeField] private WaveData[] waves;
        [SerializeField] private Transform[] enemySpawnPoints;

        private int _currentWaveIndex;
        private static WaveManager _instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance != null)
                Destroy(gameObject);
            else
                _instance = this;
        }

        private void Start()
        {
            Sun.SunDown += SpawnWave;
            SceneManager.activeSceneChanged += (oldScene, newScene) => _currentWaveIndex = 0;
        }

        public void SpawnWave()
        {
            waveDisplay.ShowCurrentWave(_currentWaveIndex);
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
                    Transform spawnPosition;
                    switch (spawnData.spawnLocation)
                    {
                        case SpawnData.SpawnLocation.Left:
                            spawnPosition = enemySpawnPoints[0];
                            break;
                        case SpawnData.SpawnLocation.Right:
                            spawnPosition = enemySpawnPoints[1];
                            break;
                        case SpawnData.SpawnLocation.Both:
                            spawnPosition = enemySpawnPoints[i % enemySpawnPoints.Length];
                            break;
                        default:
                            spawnPosition = enemySpawnPoints[0];
                            break;
                    }

                    Enemy newEnemy = Instantiate(spawnData.enemy);
                    newEnemy.transform.position = spawnPosition.position;

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
            public enum SpawnLocation
            {
                Left,
                Right,
                Both
            }

            public Enemy enemy;
            public int numberOfSpawns;

            public SpawnLocation spawnLocation;

            public float spawnDelayPerUnit;
        }
    }
}