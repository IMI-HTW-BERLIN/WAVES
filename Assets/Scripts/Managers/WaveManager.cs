using System.Collections;
using Entities.Enemies;
using UnityEngine;
using Utils;
using World;

namespace Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Transform[] enemySpawnPoints;

        private void OnEnable() => Sun.SunDown += SpawnWave;

        private void OnDisable() => Sun.SunDown -= SpawnWave;

        public void SpawnWave()
        {
            StartCoroutine(Spawning());

            IEnumerator Spawning()
            {
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Enemy newEnemy = Instantiate(enemy, enemySpawnPoints[i % enemySpawnPoints.Length].position,
                        Quaternion.identity, this.transform);
                    newEnemy.SetPlayerBase(GameManager.Instance.PlayerBase.transform);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }
        }
    }
}