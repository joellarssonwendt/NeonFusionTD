using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TutorialTower : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    public EnemySpawner EnemySpawner;
    public LevelManager LevelManager;
    
    public int currentWave;

    public GameObject TutorialgigaTower;


    float torn1x = -4.505f;
    
    float torn2x = -2.699999998f;
    float torn3x = -0.8999983f;

    float tornY = 8.15000002f;
    float tornZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);

        
        
        
    }

    // Update is called once per frame
    void Update()
    {

        int currentWave = EnemySpawner.currentWave;


        //Runda 1 instantiera första tornet, tile 42 (koordinater -4.505, 8.15000002, 0)
        if (currentWave == 1)
        {
            Vector3 torn1 = new Vector3(torn1x, tornY, 0);
            Instantiate(this, torn1, Quaternion.identity);
        }
        // runda 2 instantiera andra tornet, tile 62 (koordinater -2.699999998, 8.15000002, 0)
        if (currentWave == 2)
        {
            Vector3 torn2 = new Vector3(torn2x, tornY, 0);
            Instantiate(this, torn2, Quaternion.identity);
        }

        //runda 3 instantiera uppgraderat torn på andra tornets plats, ta bort första tornet
        if (currentWave == 3)
        {
            Vector3 torn3 = new Vector3(torn3x, tornY, 0);
            Instantiate(this, torn3, Quaternion.identity);
        }




        //runda 4 Flytta tornet till tile 82(-0.8999983, 8.150002, 0)  ta bort tornen från 42 & 62

        //runda 5 deaktivera spelobjekten
    }
}
