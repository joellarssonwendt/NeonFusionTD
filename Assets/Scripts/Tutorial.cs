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
    public EnemySpawner EnemySpawner;
    public BuildManager BuildManager;
    public GameObject GhostHand;
    
    public LevelManager LevelManager;
    public int currentWave;

    public bool getWaveActive;


    
   

    void Start()
    {
        gameObject.SetActive(true);
        // Start the ghost hand animation loop
        animator = GetComponent<Animator>();
       // EnemySpawner = GetComponent<EnemySpawner>();
       // currentWave = GetComponent<EnemySpawner>().currentWave;
        

    }

    private void Update()
    {
       //animator.SetInteger("thiswave", currentWave);

        int currentWave = EnemySpawner.currentWave;
        bool getWaveActive = EnemySpawner.getWaveActive;

       /* if (EnemySpawner.getWaveActive == getWaveActive)
        {
            gameObject.SetActive(false);
        }
        if (EnemySpawner.getWaveActive != getWaveActive)
        {
            gameObject.SetActive(true);
        }
       */

      if (gameObject.activeSelf)
        {
            
        }

        if (currentWave == 1) 
        {
            //gameObject.SetActive(true);
            StartCoroutine(Placing());
        }

        if (currentWave == 2 )
        {
            //gameObject.SetActive(true);
            StartCoroutine(PlaceNew());
        }

        if (currentWave == 3)
        {
           // gameObject.SetActive(true);
            StartCoroutine(Merging());
            if (GameObject.Find("KineticTower(clone)") != null)
            {
                StopCoroutine(PlaceNew());
                StartCoroutine(Merging());
            }
        }
        if (currentWave == 4)
        {
            //gameObject.SetActive(true);
            StartCoroutine(Moving());
        }

        
    }
   
    private IEnumerator Placing()
    {
        animator.SetTrigger("GhostHandPlace");
        yield return new WaitForSeconds((5));

    }

    private IEnumerator Merging()
    {
        animator.SetTrigger("GhostHandPlaceNew");
        yield return new WaitForSeconds((5));
        animator.SetTrigger("GhostHandMerging");
    }
    
    private IEnumerator Moving()
    {
        animator.SetTrigger("GhostHandMoving");
            yield return new WaitForSeconds((5));
    }

    private IEnumerator PlaceNew()
    {
        animator.SetTrigger("GhostHandPlaceNew");
        yield return new WaitForSeconds((5));
    }

    /* private void newWave()
     {
         int wave = thisWave;
         if (enemiesAlive == 0 ||  enemiesLeftToSpawn == 0)
         {
             thisWave++;
         }

     }
     private IEnumerator PlayGhostHandAnimation()
     {
             animator.Play("GhostHandPlacing");
             yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
     }

     private IEnumerator PlayMergeHandAnimation()
     {

             animator.Play("GhostHandMerging");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

     }

     public void startMergeTutorial()
     {
         if (currentWave == 2)
         {
             StartCoroutine(PlayMergeHandAnimation());
         }
         else
         {
             StopGhostHand();
         }
     }

     public void startTutorial()
     {
         if (currentWave == 1)
         {
             StartCoroutine(PlayGhostHandAnimation());
         }
         else
         {
             StopGhostHand();
         }
     }
     public void StopGhostHand()
     {

         animator.StopPlayback(); // Optional: Stop the animator immediately
         gameObject.SetActive(false); // Hide the ghost hand
     }*/
}
