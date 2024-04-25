using UnityEngine;

public class TimeScaleToggle : MonoBehaviour
{
    public void ToggleTimeScale()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f; 
        }
        else if (Time.timeScale == 2f)
        {
            Time.timeScale = 3f;
        } 
        else
        {
            Time.timeScale = 1f;
        }
    }
}
