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
    private Sprite initialCountdownSprite;

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
        initialCountdownSprite = countdownRing.sprite;
        StartCountdown(0f); 
    }

    public void StartCountdown(float delay)
    {
        if (!enemySpawner.activeRoundPlaying)
        {
            StartCoroutine(CountdownToNextWave(delay));
        }
    }

    private IEnumerator CountdownToNextWave(float delay)
    {
        if(optionsMenu.autoPlayNextWaveToggle.isOn) 
        {
            yield return new WaitForSecondsRealtime(delay);

            for (int i = 4; i > 0; i--)
            {
                countdownRing.sprite = countdownSprites[i - 1];
                yield return new WaitForSecondsRealtime(1f);
            }
            enemySpawner.StartWave();
            countdownRing.sprite = initialCountdownSprite;
        }
    }
}

