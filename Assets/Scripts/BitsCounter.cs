using System.Collections;
using TMPro;
using UnityEngine;

public class BitsCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int CountFPS = 60;
    public float Duration = 2f;
    private int _Value = 0;

    // Property to get or set the bits value and update the UI text
    public int Value
    {
        get => _Value;
        set
        {
            UpdateText(value);
            _Value = value;
        }
    }

    private Coroutine CountingCoroutine;     // Coroutine reference for the counting animation

    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
        PlayerStats.OnBitsChanged += UpdateBitsDisplay;      // Subscribe to the OnBitsChanged event from PlayerStats

        StartCoroutine(DelayedUpdateBitsDisplay());
    }

    private void OnDestroy()
    {
        PlayerStats.OnBitsChanged -= UpdateBitsDisplay;
    }

    private void UpdateText(int newValue)
    {
        CountingCoroutine = StartCoroutine(CountText(newValue));
    }

    public void UpdateBitsDisplay(int newValue)
    {
        Debug.Log($"Updating Bits Display to: {newValue}");
        Value = newValue; 
    }
    private IEnumerator DelayedUpdateBitsDisplay()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateBitsDisplay(PlayerStats.Bits);
    }

    // Coroutine to animate the counting of the text from the current value to the new value
    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int lastValue = _Value;
        int stepAmount;
        int difference = newValue - lastValue;

        // Determine the step amount based on whether the difference is positive or negative
        if (difference < 0) 
        {
            stepAmount = Mathf.FloorToInt((difference) / (CountFPS * Duration));
        }

        else
        {
            stepAmount = Mathf.CeilToInt((difference) / (CountFPS * Duration));

        }

        // Loop through the number of steps required to reach the new value
        for (int i = 0; i < Mathf.Abs(difference); i++)
        {
            lastValue += stepAmount;

            // Ensure the last value does not overshoot the new value
            if ((lastValue > newValue && difference > 0) || (lastValue < newValue && difference < 0))
            {
                lastValue = newValue;

            }

            if (Text != null)
            {
                Text.SetText(lastValue.ToString());
            }
            yield return Wait;
        }
    }
}
