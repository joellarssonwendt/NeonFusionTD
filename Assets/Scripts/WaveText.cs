using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    EnemySpawner enemySpawner;
    private TextMeshProUGUI currentWaveText;
    void Start()
    {
        enemySpawner = EnemySpawner.instance;
        currentWaveText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWaveText.text = enemySpawner.currentWave.ToString();
    }
}
