using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour, IDataPersistence
{
    public static EnemySpawner instance;
    public OptionsMenu optionsMenu;
    public RoundAndTimeToggle roundAndTimeToggle;
    public AutoWaveCountdown autoWaveCountdown;
    public GameObject autoWaveCountdownSprite;
    AudioManager audioManager;

    public GameObject GhostHand, tutorialtower1, tutorialtower2, tutorialtower3, tutorialtower4;
    

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
    [SerializeField] private GameObject lockIce, lockLightning, lockFire;
    [SerializeField] public GameObject bossHealthObject;
    [SerializeField] public Slider bossHealthSlider;
    [SerializeField] private GameObject infinityBoss;
    [SerializeField] private GameObject levelAchievementsGameObject;
    private LevelAchievements levelAchievements;



    public int currentWave = 1;
    private int bitsGainPerRound = 100;
    private int crystalGainPerRound = 2;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public bool activeRoundPlaying = false;
    public float previousTimeScale = 1f;
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
        audioManager = AudioManager.instance;
        Invoke("UpdateResourcesAfterCurrentWave", 0.1f);
        levelAchievements = levelAchievementsGameObject.GetComponent<LevelAchievements>();
    }
    void Update()
    {
        //Debug.Log(bitsGainPerRound.ToString());
        //Debug.Log(crystalGainPerRound.ToString());
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
        Time.timeScale = previousTimeScale;
        if (currentWave <= handCraftedWaves.Count) enemiesPerSecond = handCraftedWaves[currentWave - 1].enemiesPerSecond;
        enemiesPerSecond += (currentWave * 0.05f);

        isSpawning = true;
        activeRoundPlaying = true;
        autoWaveCountdownSprite.SetActive(false);
        enemiesLeftToSpawn = EnemiesPerWave();
        roundAndTimeToggle.SetTimeScaleSprite();
        roundAndTimeToggle.OnNextWaveStarted();
        audioManager.Stop("Countdown");
        autoWaveCountdown.ResetCountdownSprite();


        //GhostHand.SetActive(false);
        //tutorialtower1.SetActive(false);
        //tutorialtower2.SetActive(false);
        //tutorialtower3.SetActive(false);
        //tutorialtower4.SetActive(false);
    }

    private void EndWave()
    {
        Debug.Log("Wave Ended!");
        isSpawning = false;
        activeRoundPlaying = false;
        autoWaveCountdownSprite.SetActive(true);
        bossHealthObject.SetActive(false);
        timeSinceLastSpawn = 0f;
        currentWave++;
        audioManager.GetComponent<AudioManager>().PlayUISoundEffect("NewWave");

        //GhostHand.SetActive(true);
        //tutorialtower1.SetActive(true);
        //tutorialtower2.SetActive(true);
        //tutorialtower3.SetActive(true);
        //tutorialtower4.SetActive(true);

        previousTimeScale = Time.timeScale;
        Time.timeScale = 1f;

        PlayerStats.AddBits(bitsGainPerRound);
        PlayerStats.AddCrystals(crystalGainPerRound);
        if(bitsGainPerRound < 500)
        {
            bitsGainPerRound += 10;
        }
        if(crystalGainPerRound < 10)
        {
            crystalGainPerRound += 1;
        }

        nextRoundButton.SetActive(true);
        onRoundEnd.Invoke();
        CheckAndUpdateShopButtons();
        audioManager.Stop("Flamethrower");
        audioManager.Stop("TeslaTower");
        audioManager.Stop("Arctic");
        autoWaveCountdown.ResetCountdownSprite();

        if (currentWave == 31 || currentWave % 5 == 0)
        {
            levelAchievements.enableLevelPopUp(currentWave);
        }

        if (optionsMenu.autoPlayNextWaveToggle.isOn)
        {
            roundAndTimeToggle.UpdateButtonSprite();
        }

        else
        {
            Debug.Log("Auto-start next wave is disabled");
        }
    }

    private int EnemiesPerWave()
    {
        if (currentWave > handCraftedWaves.Count && currentWave % 5 == 0)
        {
            return 1;
        }
        else if (currentWave > handCraftedWaves.Count)
        {
            return Mathf.RoundToInt(baseAmount * Mathf.Pow(currentWave, difficultyScalingFactor));
        }

        // Calculate amount of enemies
        int enemiesPerWave = 0;

        for (int i = 0; i < handCraftedWaves[currentWave - 1].enemyTypes.Count; i++)
        {
            enemiesPerWave += handCraftedWaves[currentWave - 1].enemyAmounts[i];
        }
        return enemiesPerWave;
    }

    private void SpawnEnemy()
    {
        if (enemiesLeftToSpawn <= 0) return;

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

        if (currentWave > handCraftedWaves.Count && currentWave % 5 == 0)
        {
            enemyToSpawn = infinityBoss;
        }

        Instantiate(enemyToSpawn, LevelManager.main.spawnPoint.position, Quaternion.identity);
    }
    private void CheckAndUpdateShopButtons()
    {
        if (currentWave >= 5)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableIceTowerButton();
            lockIce.SetActive(false);
        }
        if (currentWave >= 9)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableLightningTowerButton();
            lockLightning.SetActive(false);
        }
        if (currentWave >= 13)
        {
            shopNormalTurretButton.GetComponent<ShopTurretButton>().EnableFireTowerButton();
            lockFire.SetActive(false);
        }
    }

    public void NotifyTimeScaleChange(float timeScale)
    {
        roundAndTimeToggle.UpdateButtonSpriteBasedOnTimeScale(timeScale);
    }
    private void UpdateResourcesAfterCurrentWave()
    {
        if (currentWave != 1)
        {
            bitsGainPerRound = Mathf.Min(bitsGainPerRound + 10 * (currentWave - 1), 500);
            crystalGainPerRound = Mathf.Min(crystalGainPerRound + 1 * (currentWave - 1), 10);
        }
    }


    public int CurrentWave
    {
        get {
            return currentWave;
        }

    }
    public bool getWaveActive
    {
        get
        {
            return activeRoundPlaying;
        }
    }
    private void IncreaseWave()
    {
        currentWave++;

    }

}
