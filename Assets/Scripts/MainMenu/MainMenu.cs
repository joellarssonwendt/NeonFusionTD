using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    DataPersistenceManager dataPersistenceManager;
    [SerializeField] private GameObject continueButtonHolder;
    private Button continueButton;
    private void Start()
    {
        dataPersistenceManager = DataPersistenceManager.instance;
        continueButton = continueButtonHolder.GetComponent<Button>();
        if (dataPersistenceManager.noData)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }
   
    public void PlaySavedGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayNewGame()
    {
        dataPersistenceManager.LoadNewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
