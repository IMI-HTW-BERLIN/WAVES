﻿using System.Collections;
using Entities.Enemies;
using UnityEngine;
using World;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float spawnDelay;
        [SerializeField] private Transform[] enemySpawnPoints;

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
                    Enemy newEnemy = Instantiate(enemy, enemySpawnPoints[i % enemySpawnPoints.Length].position,
                        Quaternion.identity, this.transform);
                    newEnemy.SetPlayerBase(GameManager.Instance.PlayerBase.transform);
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            StartCoroutine(Spawning());
        }
    }
}