using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenuUI;
    public GameObject inGameSettingsUI;
    public OptionsMenu settingsMenu;
    public DataPersistenceManager dataPersistenceManager;

    private float originalTimeScale;

    private void Start()
    {
        inGameMenuUI.SetActive(false);
    }

    public void toggleMenu()
    {
        if(!inGameMenuUI.activeSelf )
        {
            originalTimeScale = Time.timeScale;
        }
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);

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
        StartCoroutine(SaveAndExit());
    }

    private IEnumerator SaveAndExit()
    {
        dataPersistenceManager.SaveGame();
        Time.timeScale = 1;
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
