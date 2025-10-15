using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public EnemyType[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossSpawnPoint;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool isFinishedSpawning;

    private void Start()
    {
        player = ServiceLocator.Get<Player>().transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);

        StartCoroutine(SpawnWave(index));
    }

    private IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if (player = null) yield break;

            EnemyType randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemyFromPool = EnemyPool.Instance.GetPooledObject(randomEnemy);
            enemyFromPool.transform.position = randomSpot.position;
            enemyFromPool.transform.rotation = randomSpot.rotation;
            enemyFromPool.SetActive(true);

            if (i == currentWave.count - 1) isFinishedSpawning = true;
            else isFinishedSpawning = false;
            
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if(isFinishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            isFinishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
            }
        }
    }
}
