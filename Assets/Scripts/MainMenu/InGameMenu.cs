using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenuUI;

    private void Start()
    {
        inGameMenuUI.SetActive(false);
    }

    public void toggleMenu()
    {
        inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);

        Time.timeScale = inGameMenuUI.activeSelf ? 0.0f : 1.0f;
    }

    public void ContinueGame()
    {
        inGameMenuUI.SetActive (false);
        Time.timeScale = 1.0f;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
