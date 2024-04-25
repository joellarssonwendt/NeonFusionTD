using UnityEngine;
using UnityEngine.UI;

public class RoundAndTimeToggle : MonoBehaviour
{
    [SerializeField] private GameObject levelManagerObject;
    private EnemySpawner enemySpawner;
    private Image imgComp;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [Range(0, 10)]
    private bool isTimeScaleToggle = false;

    [SerializeField] private Sprite startRoundSprite;
    [SerializeField] private Sprite speed1xSprite;
    [SerializeField] private Sprite speed2xSprite;

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
        UpdateButtonSprite();
    }

    void OnDestroy()
    {
        EnemySpawner.onRoundEnd.RemoveListener(OnRoundEnd);
    }

    public void ToggleRoundAndTime()
    {
        if (!isTimeScaleToggle)
        {
            enemySpawner.StartWave();
            isTimeScaleToggle = true;
            Time.timeScale = 1.0f;
        }
        else
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 2f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        if (!isTimeScaleToggle)
        {
            imgComp.sprite = startRoundSprite;
        }
        else
        {
            if (Time.timeScale == 1f)
            {
                imgComp.sprite = speed1xSprite;
            }
            else
            {
                imgComp.sprite = speed2xSprite;
            }
        }
    }
}
