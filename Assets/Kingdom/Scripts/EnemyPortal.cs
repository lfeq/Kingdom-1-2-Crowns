using System.Collections;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnTimeInSeconds = 2f;
    [SerializeField] private int maxEnemies = 5;
    private int currentEnemies = 0;

    private void OnEnable()
    {
        GameManager.NightArrived += SpawnEnemy;
        GameManager.DayArrived += StopSpawningEnemies;
    }

    private void SpawnEnemy()
    {
        if(currentEnemies <= maxEnemies)
        {
            GameObject temEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            temEnemy.GetComponent<EnemyController>().portal = this;
            currentEnemies++;
        }
        StartCoroutine(WaitToSpawnEnemy());
    }

    IEnumerator WaitToSpawnEnemy()
    {
        yield return new WaitForSeconds(spawnTimeInSeconds);
        SpawnEnemy();
    }

    private void StopSpawningEnemies()
    {
        StopAllCoroutines();
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }
}
