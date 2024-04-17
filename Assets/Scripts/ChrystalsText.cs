using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChrystalsText : MonoBehaviour
{
    private TextMeshProUGUI chrystalAmountText;
    void Start()
    {
        chrystalAmountText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        chrystalAmountText.text = PlayerStats.Chrystals.ToString();
    }
}
