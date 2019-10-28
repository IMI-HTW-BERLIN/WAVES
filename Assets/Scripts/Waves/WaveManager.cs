using System.Collections;
using Entities;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Transform basePosition;

        public void SpawnWave()
        {
            StartCoroutine(Spawning());
            IEnumerator Spawning()
            {
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Enemy newEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity, this.transform).GetComponent<Enemy>();
                    newEnemy.SetTarget(basePosition);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }
        }
    }
}