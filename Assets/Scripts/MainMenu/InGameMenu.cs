using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenuUI;
    public GameObject inGameSettingsUI;
    public OptionsMenu settingsMenu;

    private float originalTimeScale;

    private void Start()
    {
        inGameMenuUI.SetActive(false);
    }

    public void toggleMenu()
    {
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);

        originalTimeScale = Time.timeScale;

        Time.timeScale = inGameMenuUI.activeSelf ? 0.0f : originalTimeScale;
    }


    public void toggleSettings()
    {
        inGameSettingsUI.SetActive(!inGameSettingsUI.activeSelf);

        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        inGameMenuUI.SetActive (false);
        Time.timeScale = originalTimeScale;
    }

    public void openSettings()
    {
        settingsMenu.UpdateSliders();
        inGameSettingsUI.SetActive (true);
        Time.timeScale = 0.0f;
    }

    public void closeSettings ()
    {
        inGameSettingsUI.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
