using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberText : MonoBehaviour
{
    [SerializeField] private GameObject levelManager;
    private TextMeshProUGUI waveNumberText;
    void Start()
    {
        waveNumberText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        waveNumberText.text = levelManager.GetComponent<EnemySpawner>().currentWave.ToString();
    }
}
