using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour, IDataPersistence
{

    [SerializeField] private GameObject audioManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;
    DataPersistenceManager dataPersistenceManager;

    public int startingHealth = 5;
    public int currentHealth = 0;
    public GameObject gameOver;
    public int passAmount = 1;


    [SerializeField] private float gameOverDelay = 5;

    // Start is called before the first frame update
    void Start()
    {
        dataPersistenceManager = DataPersistenceManager.instance;
        //currentHealth = startingHealth;
        Invoke("UpdateHealthBar", 0.1f);
    }

    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
    }
    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
    }

    private IEnumerator gameOverscreen()
    {
        gameOver.SetActive(true);
        yield return new WaitForSeconds(gameOverDelay);
        dataPersistenceManager.SaveGame();
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentHealth -= passAmount;
            healthSlider.value = currentHealth;

            UpdateHealthBar();
            Debug.Log("Hit!");
        }
    }


    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
        if (currentHealth >= 3)
        {
            fillColor.color = greenHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }

        if (currentHealth == 0)
        {
            dataPersistenceManager.playerDied = true;
            audioManager.GetComponent<AudioManager>().PlaySoundEffect("GameOver");
            StartCoroutine(gameOverscreen());
        }
    }

    
    
}
