using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAchievements : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;
    [SerializeField] private GameObject healthManager;
    [SerializeField] private GameObject levelManager;
    [SerializeField] private GameObject endlessPopUp;
    [SerializeField] private GameObject numberImageHolder;
    [SerializeField] private GameObject level30PopUp;
    [SerializeField] private GameObject level25PopUp;
    [SerializeField] private GameObject level20PopUp;
    [SerializeField] private GameObject level15PopUp;
    [SerializeField] private GameObject level10PopUp;
    [SerializeField] private GameObject level5PopUp;
    private int levelCount;
    private static System.Random random = new System.Random();
    private EnemySpawner enemySpawner;
    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        enemySpawner = levelManager.GetComponent<EnemySpawner>();
    }
    public void enableLevelPopUp(int levelNumber)
    {
        Debug.Log(levelNumber);
        if(healthManager.GetComponent<HealthSystem>().currentHealth <= 0)
        {
            return;
        }

        if(levelNumber == 31)
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
            level15PopUp.SetActive(true);
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
            if (levelNumber > 31 && levelNumber % 5 == 0)
            {
                ShowEndlessRoundAchievement();
            }
        }
        Invoke("DisableLevelPopUp", 2.5f);
    }
    public void DisableLevelPopUp()
    {
        endlessPopUp.SetActive(false);
        level30PopUp.SetActive(false);
        level25PopUp.SetActive(false);
        level20PopUp.SetActive(false);
        level15PopUp.SetActive(false);
        level10PopUp.SetActive(false);
        level5PopUp.SetActive(false);
    }

    public void ShowEndlessRoundAchievement()
    {
            levelCount = enemySpawner.currentWave;
            numberImageHolder.GetComponent<TextMeshProUGUI>().text = levelCount.ToString();
            endlessPopUp.SetActive(true);

            int randomInt = GenerateRandomInt(1, 5);
            PickRandomAchievementSound(randomInt);
    }
    private int GenerateRandomInt(int minNum, int maxNum)
    {
        int randomIntInRange = random.Next(minNum, maxNum);
        return randomIntInRange;
    }
    private void PickRandomAchievementSound(int number)
    {
        if(number == 1)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement1");
        }
        else if(number == 2)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement2");
        }
        else if( number == 3)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement3");
        }
        else if (number == 4)
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement4");
        }
        else
        {
            audioManager.GetComponent<AudioManager>().PlayUISoundEffect("roundAchievement5");
        }
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
