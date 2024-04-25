using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BitsText : MonoBehaviour
{
    private TextMeshProUGUI bitsAmountText;
    void Start()
    {
        bitsAmountText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        bitsAmountText.text = PlayerStats.Bits.ToString();
    }
}
