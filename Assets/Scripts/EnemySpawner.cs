using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private int baseAmount = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseAmount * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyTypes[Random.Range(0, enemyTypes.Length)];
        Instantiate(enemyToSpawn, LevelManager.main.spawnPoint.position, Quaternion.identity);
    }
}
