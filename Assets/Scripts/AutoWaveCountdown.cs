using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoWaveCountdown : MonoBehaviour
{
    public static AutoWaveCountdown Instance { get; private set; }

    public EnemySpawner enemySpawner;
    public OptionsMenu optionsMenu;
    public Image countdownRing; 
    public Sprite[] countdownSprites;
    [SerializeField] private Sprite initialCountdownSprite;
    AudioManager audioManager;
    private Coroutine countdownCoroutine;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ResetCountdownSprite();
        StartCountdown(0f);
        optionsMenu.autoPlayNextWaveToggle.onValueChanged.AddListener(OnAutoPlayNextWaveToggleChanged);
    }

    private void OnDisable()
    {
        optionsMenu.autoPlayNextWaveToggle.onValueChanged.RemoveListener(OnAutoPlayNextWaveToggleChanged);
    }

    public void StartCountdown(float delay)
    {
        if (!enemySpawner.activeRoundPlaying)
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
            countdownCoroutine = StartCoroutine(CountdownToNextWave(delay));
        }
    }

    public void ResetCountdownSprite()
    {
        countdownRing.sprite = initialCountdownSprite;
    }

    private IEnumerator CountdownToNextWave(float delay)
    {
        if (optionsMenu.autoPlayNextWaveToggle.isOn)
        {
            audioManager.PlayUISoundEffect("Countdown");

            yield return new WaitForSecondsRealtime(delay);

            for (int i = 4; i > 0; i--)
            {
                countdownRing.sprite = countdownSprites[i - 1];
                yield return new WaitForSecondsRealtime(1.5f);
            }
            enemySpawner.StartWave();
            ResetCountdownSprite();
        }
    }

    private void OnAutoPlayNextWaveToggleChanged(bool isOn)
    {
        if (!isOn && countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            audioManager.Stop("Countdown");
            countdownCoroutine = null;
            ResetCountdownSprite();
        }
    }
}

