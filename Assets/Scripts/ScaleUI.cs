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
            Debug.Log("Nu händer de grejer för skärmen är bred.");
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
