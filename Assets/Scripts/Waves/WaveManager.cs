using System.Collections;
using Entities;
using UnityEngine;
using World;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Transform basePosition;

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
                    Enemy newEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity, this.transform)
                        .GetComponent<Enemy>();
                    newEnemy.SetTarget(basePosition);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            StartCoroutine(Spawning());
        }
    }
}