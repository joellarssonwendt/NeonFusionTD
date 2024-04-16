using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class diamondBits : MonoBehaviour
{
    private TextMeshPro bitsAmountText;
    public int bitsAmount;
    private bool hasStarted = false;
    void Start()
    {
        bitsAmountText = GetComponent<TextMeshPro>();
        bitsAmount = PlayerStats.Chrystals;
        hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted && bitsAmountText != null)
        {
            bitsAmountText.text = bitsAmount.ToString();
        }
    }
}
