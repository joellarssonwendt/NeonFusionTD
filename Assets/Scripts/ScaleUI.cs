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
            Debug.Log("Nu händer de grejer för skärmen är bred.");
            diamondIcon.transform.position = new Vector3(diamondIcon.transform.position.x, diamondIcon.transform.position.y + moveUpperIconsValue, diamondIcon.transform.position.z);
            menuIcon.transform.position = new Vector3(menuIcon.transform.position.x, menuIcon.transform.position.y + moveUpperIconsValue, menuIcon.transform.position.z);
        }
        else
        {
            Debug.Log("Nu händer inget för skärmen är smal som på mobil");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
