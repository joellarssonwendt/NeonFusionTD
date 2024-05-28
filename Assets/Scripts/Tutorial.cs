using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    private Animator animator;

    public GameObject GhostHand;
    public EnemySpawner EnemySpawner;
    public LevelManager LevelManager;


    public Graphic hand, tower1, tower2, towerMove, towerMerge;
 
    public GameObject TutorialTower1;
    public GameObject TutorialTower2;

   /* public GameObject TutorialgigaTower;
    public GameObject TutorialgigaTower2;
   */

    /* Potentiell lösning:
    Lägg till image enable 1.5 sekunder på varje ände av animationerna, samt börja och sluta allting med gameobject.setactive true
    Hopefully this fucking works */
    void Start()
    {
        
        animator = GetComponent<Animator>();
        

    }

    private void Update()
    {
       
        int currentWave = EnemySpawner.currentWave;
        bool getWaveActive = EnemySpawner.getWaveActive;
      
        if (currentWave == 1) 
        {
            
            StartCoroutine(Placing());
            
        }

        if (currentWave == 2)
        {
            
            StartCoroutine(PlaceNew());
            
        }
        if (currentWave == 3)
        {

            

        }

        if (currentWave == 4)
        {
            
            StartCoroutine(Merging());
           
        }

        if (currentWave >= 5)
        {
            GhostHand.SetActive(false);
            TutorialTower1.SetActive(false);
            TutorialTower2.SetActive(false);
          
        }
    }
   
    private IEnumerator Placing()
    {

        
        int normalTowers = GameObject.FindGameObjectsWithTag("normal").Length;
        yield return new WaitForSeconds((0f));
        animator.SetTrigger("GhostHandPlace");
        animator.SetTrigger("towerIdle");
        

        if (normalTowers == 1)
        {
            animator.SetInteger("normalTowers", 1);
            animator.SetTrigger("GhostHandIdle");
            animator.SetTrigger("towerIdle");

           // GhostHand.SetActive(false);
            //TutorialTower1.SetActive(false);
        }
    }

    private IEnumerator PlaceNew()
    {
       
        int normalTowers = GameObject.FindGameObjectsWithTag("normal").Length;
        yield return new WaitForSeconds((0f));
        animator.SetTrigger("GhostHandPlaceNew");
        animator.SetTrigger("towerIdle");


        if (normalTowers == 2)
        {
            animator.SetInteger("normalTowers", 2);
            animator.SetTrigger("GhostHandIdle");
            animator.SetTrigger("towerIdle");

           // GhostHand.SetActive(false);
            //TutorialTower2.SetActive(false);
        }
    }

    
    private IEnumerator Merging()
    {

        
        int bigTurret = GameObject.FindGameObjectsWithTag("supernormal").Length; 
        yield return new WaitForSeconds((0f));
        animator.SetTrigger("GhostHandMerging");
        animator.SetTrigger("towerIdle");


        if (bigTurret == 1)
        {
            
            animator.SetInteger("bigTurret", 1);
            animator.SetTrigger("GhostHandIdle");
            animator.SetTrigger("towerIdle");

            GhostHand.SetActive(false);
            //TutorialgigaTower.SetActive(false);
        }
    }


/*
    private IEnumerator Merging()
    {
        

        yield return new WaitForSeconds((0f));
        animator.SetTrigger("GhostHandMerging");
        
        

        int gigaTowers = GameObject.FindGameObjectsWithTag("supernormal").Length;
        if (gigaTowers == 1)
        {
            
            TutorialgigaTower2.SetActive(false);
        }
    }
*/

    
}

