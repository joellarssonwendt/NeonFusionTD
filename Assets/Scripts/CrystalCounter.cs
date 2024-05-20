using System.Collections;
using TMPro;
using UnityEngine;

public class CrystalCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int CountFPS = 60;
    public float Duration = 20f;
    private int _Value = 0;

    public int Value
    {
        get => _Value;
        set
        {
            UpdateText(value);
            _Value = value;
        }
    }

    private Coroutine CountingCoroutine;

    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
        PlayerStats.OnCrystalsChanged += UpdateCrystalsDisplay;
        StartCoroutine(DelayedUpdateCrystalsDisplay());
    }

    private void OnDestroy()
    {
        PlayerStats.OnCrystalsChanged -= UpdateCrystalsDisplay;
    }

    private void UpdateText(int newValue)
    {
        CountingCoroutine = StartCoroutine(CountText(newValue));
    }
    private void UpdateCrystalsDisplay(int newValue)
    {
        Debug.Log($"Updating Crystals Display to: {newValue}");
        Value = newValue;
    }
    private IEnumerator DelayedUpdateCrystalsDisplay()
    {
        yield return new WaitForSeconds(0.1f);

        UpdateCrystalsDisplay(PlayerStats.Crystals);
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int lastValue = _Value;
        int stepAmount;
        int difference = newValue - lastValue;

        if (difference < 0) 
        {
            stepAmount = Mathf.FloorToInt((difference) / (CountFPS * Duration));
        }

        else
        {
            stepAmount = Mathf.CeilToInt((difference) / (CountFPS * Duration));

        }

        for (int i = 0; i < Mathf.Abs(difference); i++)
        {
            lastValue += stepAmount;

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
