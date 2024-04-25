using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour//, IDataPersistence
{
    public static EnemySpawner instance;
    // Events
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent onRoundEnd = new UnityEvent();

    // Variables
    [SerializeField] private List<HandCraftedWave> handCraftedWaves;
    [SerializeField] private GameObject[] randomEnemyArray;
    [SerializeField] private int baseAmount = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("referenser")]
    [SerializeField] private GameObject nextRoundButton;
    

    private int currentWave = 1;
    private int chrystalGainPerRound = 100;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public bool activeRoundPlaying = false;
    int enemyAmountCounter = 0;
    int enemyTypeCounter = 0;

    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
     
        if (instance != null)
        {
            Debug.Log("Det finns redan en EnemySpawner");
            return;
        }
        instance = this;
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

        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

   /* public void LoadData(GameData data)
    {
        this.currentWave = data.currentWave;
    }
    public void SaveData(ref GameData data)
    {
        data.currentWave = this.currentWave;
    }*/

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        Debug.Log("Enemies Alive: " + enemiesAlive);
    }

    public void StartWave()
    {
        Debug.Log("Wave Started!");

        if (currentWave <= handCraftedWaves.Count) enemiesPerSecond = handCraftedWaves[currentWave-1].enemiesPerSecond;

        isSpawning = true;
        activeRoundPlaying = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        Debug.Log("Wave Ended!");
        isSpawning = false;
        activeRoundPlaying = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        PlayerStats.Chrystals += chrystalGainPerRound;
        chrystalGainPerRound += 10;
        nextRoundButton.SetActive(true);
        onRoundEnd.Invoke();
    }

    private int EnemiesPerWave()
    {
        if (currentWave > handCraftedWaves.Count) return Mathf.RoundToInt(baseAmount * Mathf.Pow(currentWave, difficultyScalingFactor));

        int enemiesPerWave = 0;
        for (int i = 0; i < handCraftedWaves[currentWave-1].enemyTypes.Length; i++)
        {
            enemiesPerWave += handCraftedWaves[currentWave-1].enemyAmounts[i];
        }
        return enemiesPerWave;
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = null;

        if (currentWave <= handCraftedWaves.Count)
        {
            // Håll ordning på vilken enemyAmount vi är på
            enemyAmountCounter++;

            if (enemyAmountCounter >= handCraftedWaves[currentWave-1].enemyAmounts.Length-1)
            {
                // Håll ordning på vilken enemyType vi är på
                enemyTypeCounter++;
                enemyAmountCounter = 0;

                if (enemyTypeCounter > handCraftedWaves[currentWave-1].enemyTypes.Length-1)
                {
                    // Återställ när waven är slut
                    enemyTypeCounter = 0;
                    enemyAmountCounter = 0;
                }
            }

            enemyToSpawn = handCraftedWaves[currentWave-1].enemyTypes[enemyTypeCounter];
        }
        else
        {
            enemyToSpawn = randomEnemyArray[Random.Range(0, randomEnemyArray.Length)];
        }

        Instantiate(enemyToSpawn, LevelManager.main.spawnPoint.position, Quaternion.identity);
    }
}
