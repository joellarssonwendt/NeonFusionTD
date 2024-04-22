using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{

    [SerializeField] private GameObject diamondIcon;
    [SerializeField] private GameObject menuIcon;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject mergeArchive;

    private float moveUpperIconsValue = 80f;
    void Start()
    {

        float aspectRatio = (float)Screen.width / Screen.height;
        float aspectRatioWidthLimit = 9f / 16f;

        if (aspectRatio > aspectRatioWidthLimit)
        {
            Debug.Log("Nu h�nder de grejer f�r sk�rmen �r bred.");
            diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + moveUpperIconsValue, diamondIcon.transform.position.z);
            menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y + moveUpperIconsValue, menuIcon.transform.position.z);
        }
        else
        {
            Debug.Log("Nu h�nder inget f�r sk�rmen �r smal som p� mobil");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
