using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class TouchDrawer : MonoBehaviour
{
    public GameObject lineprefab;
    public LineRenderer lineRenderers;
    Coroutine drawing;



    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FinishLine();

        }
    }

    void StartLine()
    {
        if (drawing != null)
        {
            StopCoroutine(drawing);
        }
        drawing = StartCoroutine(DrawLine());
    }
   
    void FinishLine()
    {
        StopCoroutine(drawing);
        
    }

    IEnumerator DrawLine()
    {

        

        GameObject lineObject = Instantiate(lineprefab, new Vector3(0, 0, -6), Quaternion.identity);
        LineRenderer line = lineObject.GetComponent<LineRenderer>();
        line.positionCount = 0;

        while (true)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, position);
            yield return null;
        }
        
    }

   
}