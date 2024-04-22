using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float aspectRatioWidthLimit = 9f / 16f;

        if(aspectRatio > aspectRatioWidthLimit)
        {
            Debug.Log("Nu h�nder de grejer f�r sk�rmen �r bred.");
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
