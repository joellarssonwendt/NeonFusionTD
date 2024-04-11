using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;


    public int startingHealth = 5;
    public int currentHealth = 0;

    public int passAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }




    /*     if (currentHealth <= 0)
         {
             Restart scene?
         }

 */


   /* public void EnemyPass(int passAmount)
    {
        currentHealth -= passAmount;
        healthSlider.value = currentHealth;

        UpdateHealthBar();
    }*/

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
        if (currentHealth >= 2)
        {
            fillColor.color = greenHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }
    }


    
}
