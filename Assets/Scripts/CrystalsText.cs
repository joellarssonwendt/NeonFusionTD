using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalsText : MonoBehaviour
{
    private TextMeshProUGUI crystalAmountText;
    void Start()
    {
        crystalAmountText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        crystalAmountText.text = PlayerStats.Crystals.ToString();
    }
}
