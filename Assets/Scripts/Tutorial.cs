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
    public int currentWave;

    public bool getWaveActive;

    public GameObject TutorialTower1;
    public GameObject TutorialTower2;

    public GameObject TutorialgigaTower;
    public GameObject TutorialgigaTower2;



    

    void Start()
    {
        if(currentWave < 5) 
        {
            gameObject.SetActive(true);
            animator = GetComponent<Animator>();

        }

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
            StartCoroutine(Moving());          
        }
        if (currentWave == 4)
        {
            StartCoroutine(Merging());
        }

        if (currentWave >= 5)
        {
            GhostHand.SetActive(false);
        }
    }
   
    private IEnumerator Placing()
    {
        int normalTowers = GameObject.FindGameObjectsWithTag("normal").Length;
        yield return new WaitForSeconds((5f));
        animator.SetTrigger("GhostHandPlace");

        if (normalTowers == 1)
        {
            GhostHand.SetActive(false);

        }


    }
    private IEnumerator PlaceNew()
    {
        int normalTowers = GameObject.FindGameObjectsWithTag("normal").Length;
        yield return new WaitForSeconds((3.5f));
        animator.SetTrigger("GhostHandPlaceNew");
        if (normalTowers == 2)
        {
            GhostHand.SetActive(false);

        }
    }


    private IEnumerator Merging()
    {
        int gigaTowers = GameObject.FindGameObjectsWithTag("supernormal").Length;

        yield return new WaitForSeconds((0.1f));
        animator.SetTrigger("GhostHandMerging");
        if (gigaTowers == 1)
        {
            GhostHand.SetActive(false);
        }
    }
    private IEnumerator Moving()
    {
        yield return new WaitForSeconds((0.1f));
        animator.SetTrigger("GhostHandMoving");

        Vector3 movedTower = new Vector3(3.291216f, 2.323133f, 5.841029f);

        if (Vector3.Distance(transform.position, movedTower) < 0.001f)
        {
            GhostHand.SetActive(false);
        }
    }

}

