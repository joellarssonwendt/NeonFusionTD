using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class diamondBits : MonoBehaviour
{
    [SerializeField] GameObject textHolder;
    private TextMeshProUGUI bitsAmountText;
    public int bitsAmount;
    private bool hasStarted = false;
    void Start()
    {
        bitsAmountText = textHolder.GetComponent<TextMeshProUGUI>();
        bitsAmount = 100;
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
