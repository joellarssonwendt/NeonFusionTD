using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;


    public int startingHealth = 5;
    public int currentHealth = 0;
    public GameObject gameOver;
    public int passAmount = 1;


    [SerializeField] private float gameOverDelay = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }


    

    private IEnumerator gameOverscreen()
    {


        gameOver.SetActive(true);
        yield return new WaitForSeconds(gameOverDelay);
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
            StartCoroutine(gameOverscreen());

            
        }
    }

    
    
}
