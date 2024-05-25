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
        gameObject.SetActive(true);
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
            StartCoroutine(Moving());          
        }
        if (currentWave == 4)
        {
            StartCoroutine(Merging());
        }

        if (currentWave == 5)
        {
            GhostHand.SetActive(false);
        }
    }
   
    private IEnumerator Placing()
    {
        yield return new WaitForSeconds((5f));
        animator.SetTrigger("GhostHandPlace");
            
    }
    private IEnumerator PlaceNew()
    {
        yield return new WaitForSeconds((3.5f));
        animator.SetTrigger("GhostHandPlaceNew");
    }
        
        
    private IEnumerator Merging()
    {
        yield return new WaitForSeconds((0.1f));
        animator.SetTrigger("GhostHandMerging");  
    }
    
    private IEnumerator Moving()
    {
        yield return new WaitForSeconds((0.1f));
        animator.SetTrigger("GhostHandMoving");       
    }
}
