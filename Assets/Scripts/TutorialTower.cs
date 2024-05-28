using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TutorialTower : MonoBehaviour
{
    private Animator animator;

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
        animator = GetComponent<Animator>();
    }
    private IEnumerator towerOne()
    {
        yield return new WaitForSeconds((5f));
        animator.SetTrigger("TutorialTowerShow1");

        if (EnemySpawner.activeRoundPlaying)
        {
            StopCoroutine(towerOne());
        }
    }
    private IEnumerator towerTwo()
    {
        yield return new WaitForSeconds((5f));
        animator.SetTrigger("TutorialTowerShow2");

        if (EnemySpawner.activeRoundPlaying)
        {
            StopCoroutine(towerTwo());
        }
    }
    private IEnumerator towerMove()
    {      
        yield return new WaitForSeconds((1f));
        animator.SetTrigger("TutorialTowerShow3");

        if (EnemySpawner.activeRoundPlaying)
        {
            StopCoroutine((towerMove()));
        }
    }
    private IEnumerator towerMerge()
    {     
        yield return new WaitForSeconds((5f));
        animator.SetTrigger("TutorialTowerShow4");

        if (EnemySpawner.activeRoundPlaying)
        {
            StopCoroutine(towerMerge());
        }
    }

    

    
    // Update is called once per frame
    void Update()
    {
        int currentWave = EnemySpawner.currentWave;
        bool getWaveActive = EnemySpawner.getWaveActive;
        //Runda 1 instantiera första tornet, tile 42 (koordinater -4.505, 8.15000002, 0)
        if (currentWave == 1)
        {
 
            StartCoroutine(towerOne());

        }

        // runda 2 instantiera andra tornet, tile 62 (koordinater -2.699999998, 8.15000002, 0)
        if (currentWave == 2)
        {
            
            StartCoroutine(towerTwo());

        }

        //runda 3 instantiera uppgraderat torn på andra tornets plats, ta bort första tornet
        if (currentWave == 3)
        {
            
            StartCoroutine(towerMove());
            
        }

        //runda 4 Flytta tornet till tile 82(-0.8999983, 8.150002, 0)  ta bort tornen från 42 & 62
        if (currentWave == 4)
        {
            
            StartCoroutine(towerMerge());

        }
        //runda 5 deaktivera spelobjekten

        if (currentWave >= 5) 
        {
            TutorialTower1.SetActive(false);
            TutorialTower2.SetActive(false);
            TutorialgigaTower.SetActive(false);
            TutorialgigaTower2.SetActive(false);

        }
    }        
}

