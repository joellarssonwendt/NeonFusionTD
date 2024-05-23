using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAchievements : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;
    [SerializeField] private GameObject levelManager;
    [SerializeField] private GameObject level30PopUp;
    [SerializeField] private GameObject level25PopUp;
    [SerializeField] private GameObject level20PopUp;
    [SerializeField] private GameObject level15PopUp;
    [SerializeField] private GameObject level10PopUp;
    [SerializeField] private GameObject level5PopUp;
    private EnemySpawner enemySpawner;
    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        enemySpawner = levelManager.GetComponent<EnemySpawner>();
    }
    public void enableLevelPopUp(int levelNumber)
    {
        //play sound
        if(levelNumber == 30)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("round30WIN");
            level30PopUp.SetActive(true);
            return;
        }
        else if(levelNumber == 25)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement5");
            level25PopUp.SetActive(true);
        }
        else if(levelNumber == 20)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement4");
            level20PopUp.SetActive(true);  
        }
        else if(levelNumber == 15)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement3");
            level20PopUp.SetActive(true);
        }
        else if(levelNumber == 10)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement2");
            level10PopUp.SetActive(true);
        }
        else if(levelNumber == 5)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement1");
            level5PopUp.SetActive(true); 
        }
        else
        {
            //här kan man lägga en som kör var 5e runda med text som skrivs in automatiskt
            //även köra ett random ljud
        }
        Invoke("DisableLevelPopUp", 5f);
    }
    public void DisableLevelPopUp()
    {
        level30PopUp.SetActive(false);
        level25PopUp.SetActive(false);
        level20PopUp.SetActive(false);
        level15PopUp.SetActive(false);
        level10PopUp.SetActive(false);
        level5PopUp.SetActive(false);
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
