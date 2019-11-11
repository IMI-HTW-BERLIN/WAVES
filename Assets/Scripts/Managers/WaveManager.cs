using System.Collections;
using Entities;
using UnityEngine;
using World;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float spawnDelay;

        private void Start()
        {
            Sun.SunDown += SpawnWave;
        }

        public void SpawnWave()
        {
            IEnumerator Spawning()
            {
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Enemy newEnemy = Instantiate(enemy, GameManager.Instance.EnemySpawnPoint.position,
                        Quaternion.identity, this.transform);
                    newEnemy.SetTarget(GameManager.Instance.PlayerBase.transform);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            StartCoroutine(Spawning());
        }
    }
}