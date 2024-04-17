using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextRoundButton : MonoBehaviour
{
    [SerializeField] private GameObject levelManagerObject;
    private EnemySpawner enemySpawner;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [Range(0, 10)]
    public float speed = 1f;
    private Image imgComp;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = levelManagerObject.GetComponent<EnemySpawner>();  
        imgComp = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imgComp.color = Color.Lerp(endColor, startColor, Mathf.PingPong(Time.time * speed, 1));
    }

    public void startNextRound()
    {
        enemySpawner.StartWave();
        gameObject.SetActive(false);
    }
}
