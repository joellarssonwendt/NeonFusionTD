using UnityEngine;
using UnityEngine.UI;

public class RoundAndTimeToggle : MonoBehaviour
{
    [SerializeField] private GameObject levelManagerObject;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private Sprite startRoundSprite;
    [SerializeField] private Sprite speed1xSprite;
    [SerializeField] private Sprite speed2xSprite;

    
    private EnemySpawner enemySpawner;
    private Image imgComp;
    public OptionsMenu optionsMenu;

    [Range(0, 10)]
    private bool isTimeScaleToggle = false;
    private float lastTimeScale = 1f;

    void Start()
    {
        enemySpawner = levelManagerObject.GetComponent<EnemySpawner>();
        imgComp = GetComponent<Image>();
        EnemySpawner.onRoundEnd.AddListener(OnRoundEnd);
    }

    void Update()
    {
        imgComp.color = Color.Lerp(endColor, startColor, Mathf.PingPong(Time.time, 1));
    }

    void OnRoundEnd()
    {
        isTimeScaleToggle = false;
        lastTimeScale = Time.timeScale;
        Time.timeScale = 1f;
        UpdateButtonSprite();
    }

    void OnDestroy()
    {
        EnemySpawner.onRoundEnd.RemoveListener(OnRoundEnd);
    }

    public void ToggleRoundAndTime()
    {
        if (!isTimeScaleToggle || !enemySpawner.activeRoundPlaying)
        {
            enemySpawner.StartWave();
            isTimeScaleToggle = true;
            Time.timeScale = enemySpawner.previousTimeScale;
        }
        else
        {
            if (Time.timeScale == 2f)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 2f;
            }
        }
        UpdateButtonSpriteBasedOnTimeScale(Time.timeScale);
    }

    public void OnNextWaveStarted()
    {
        isTimeScaleToggle = true;
        lastTimeScale = Time.timeScale;
        UpdateButtonSpriteBasedOnTimeScale(Time.timeScale);
    }

    public void UpdateButtonSprite()
    {
        if (!isTimeScaleToggle)
        {
            imgComp.sprite = startRoundSprite;
        }
        else
        {
            UpdateButtonSpriteBasedOnTimeScale(Time.timeScale);
        }
    }

    public void UpdateButtonSpriteBasedOnTimeScale(float timeScale)
    {
        if (timeScale == 1f)
        {
            imgComp.sprite = speed1xSprite;
        }
        else
        {
            imgComp.sprite = speed2xSprite;
        }
    }
    public void SetTimeScaleSprite()
    {
        UpdateButtonSpriteBasedOnTimeScale(Time.timeScale);
    }
}
