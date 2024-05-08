using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoWaveCountdown : MonoBehaviour
{
    public static AutoWaveCountdown Instance { get; private set; }

    public EnemySpawner enemySpawner;
    public Image countdownRing; 
    public Sprite[] countdownSprites; 

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
        yield return new WaitForSeconds(delay);

        for (int i = 4; i > 0; i--)
        {
            Debug.Log("Changing sprite to: " + countdownSprites[i - 1].name);
            countdownRing.sprite = countdownSprites[i - 1];
            yield return new WaitForSeconds(1f);
        }
        enemySpawner.StartWave();
    }
}

