using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenuUI;
    public GameObject inGameSettingsUI;
    public OptionsMenu settingsMenu;

    private void Start()
    {
        inGameMenuUI.SetActive(false);
    }

    public void toggleMenu()
    {
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);

        Time.timeScale = inGameMenuUI.activeSelf ? 0.0f : 1.0f;
    }

    public void toggleSettings()
    {
        inGameSettingsUI.SetActive(!inGameSettingsUI.activeSelf);

        Time.timeScale = inGameSettingsUI.activeSelf ? 0.0f : 1.0f;
    }

    public void ContinueGame()
    {
        inGameMenuUI.SetActive (false);
        Time.timeScale = 1.0f;
    }

    public void openSettings()
    {
        settingsMenu.UpdateSliders();
        inGameSettingsUI.SetActive (true);
        Time.timeScale = 1.0f;
    }

    public void closeSettings ()
    {
        inGameSettingsUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
