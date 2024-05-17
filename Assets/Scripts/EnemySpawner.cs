using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour, IDataPersistence
{
    public static EnemySpawner instance;
    public OptionsMenu optionsMenu;
    public RoundAndTimeToggle roundAndTimeToggle;
    public GameObject autoWaveCountdown;

    // Events
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent onRoundEnd = new UnityEvent();

    // Variables
    [SerializeField] private List<HandCraftedWave> handCraftedWaves;
    [SerializeField] private GameObject[] randomEnemyArray;
    [SerializeField] private int baseAmount = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float difficultyScalingFactor = 0.9f;

    [Header("referenser")]
    [SerializeField] private GameObject nextRoundButton;
    [SerializeField] private GameObject shopNormalTurretButton, shopIceTurretButton, shopLightningTurretButton, shopFireTurretButton;
    [SerializeField] public GameObject bossHealthObject;
    [SerializeField] public Slider bossHealthSlider;



    public int currentWave = 1;
    private int bitsGainPerRound = 100;
    private int crystalGainPerRound = 2;
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
    private void Start()
    {
        Invoke("CheckAndUpdateShopButtons", 0.1f);
        bossHealthObject.SetActive(false);
    }
    void Update()
    {
        if (!isSpawning) return;
        //Debug.Log(currentWave.ToString());
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

    public void LoadData(GameData data)
    {
        this.currentWave = data.currentWave;
    }
    public void SaveData(ref GameData data)
    {
        data.currentWave = this.currentWave;
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        Debug.Log("Enemies Alive: " + enemiesAlive);
    }

    public void StartWave()
    {
        Debug.Log("Wave Started!");
        if (currentWave <= handCraftedWaves.Count) enemiesPerSecond = handCraftedWaves[currentWave - 1].enemiesPerSecond;
        enemiesPerSecond += (currentWave * 0.05f);

        isSpawning = true;
        activeRoundPlaying = true;
        autoWaveCountdown.SetActive(false);
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        Debug.Log("Wave Ended!");
        isSpawning = false;
        activeRoundPlaying = false;
        autoWaveCountdown.SetActive(true);
        timeSinceLastSpawn = 0f;
        currentWave++;

        PlayerStats.AddBits(bitsGainPerRound);
        PlayerStats.AddCrystals(crystalGainPerRound);
        bitsGainPerRound += 10;
        crystalGainPerRound += 1;

        nextRoundButton.SetActive(true);
        onRoundEnd.Invoke();
        CheckAndUpdateShopButtons();


        if (optionsMenu.autoPlayNextWaveToggle.isOn)
        {
            roundAndTimeToggle.UpdateButtonSprite();
        }


        if (optionsMenu.autoPlayNextWaveToggle.isOn)
        {
            StartCoroutine(StartNextWaveAfterDelay(5f));
        }

        else
        {
            Debug.Log("Auto-start next wave is disabled");
        }
    }

    private int EnemiesPerWave()
    {
        if (currentWave > handCraftedWaves.Count) return Mathf.RoundToInt(baseAmount * Mathf.Pow(currentWave, difficultyScalingFactor));

        int enemiesPerWave = 0;
        for (int i = 0; i < handCraftedWaves[currentWave - 1].enemyTypes.Count; i++)
        {
            enemiesPerWave += handCraftedWaves[currentWave - 1].enemyAmounts[i];
        }
        return enemiesPerWave;
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = null;

        if (currentWave <= handCraftedWaves.Count)
        {
            if (enemyAmountCounter >= handCraftedWaves[currentWave - 1].enemyAmounts[enemyTypeCounter])
            {
                // Håll ordning på vilken enemyType vi är på
                enemyAmountCounter = 0;
                enemyTypeCounter++;

                if (enemyTypeCounter > handCraftedWaves[currentWave - 1].enemyTypes.Count - 1)
                {
                    // Återställ när waven är slut
                    enemyTypeCounter = 0;
                    enemyAmountCounter = 0;
                }
            }

            // Håll ordning på vilken enemyAmount vi är på
            enemyAmountCounter++;



            enemyToSpawn = handCraftedWaves[currentWave - 1].enemyTypes[enemyTypeCounter];
        }
        else
        {
            // Spawnar random fiende-typ om det inte finns några fler HandCraftedWaves
            enemyToSpawn = randomEnemyArray[Random.Range(0, randomEnemyArray.Length)];
        }

        Instantiate(enemyToSpawn, LevelManager.main.spawnPoint.position, Quaternion.identity);
    }
    private void CheckAndUpdateShopButtons()
    {
        if (currentWave >= 4)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableIceTowerButton();
        }
        if (currentWave >= 8)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableLightningTowerButton();
        }
        if (currentWave >= 12)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableFireTowerButton();
        }
    }
    private IEnumerator StartNextWaveAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        StartWave();
        NotifyTimeScaleChange(Time.timeScale);
    }

    public void NotifyTimeScaleChange(float timeScale)
    {
        roundAndTimeToggle.UpdateButtonSpriteBasedOnTimeScale(timeScale);
    }
}
