using UnityEngine;
using System.Collections;
using UnityEditor;

/*public class TouchDrawer : MonoBehaviour
{
    public GameObject linePrefab;

    
    Coroutine drawing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse Down
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out Vector2 lastLinePoint);
            mesh = MeshUtils.CreateMesh(lastLinePoint, lastLinePoint, lastLinePoint, lastLinePoint);
            lineCanvasRenderer.SetMesh(mesh);
            arrowRectTransform.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            // Mouse Held Down
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out Vector2 mouseLocalPoint);
            if (Vector2.Distance(lastLinePoint, mouseLocalPoint) > 5f)
            {
                // Far enough from last point
                Vector2 forwardVector = (mouseLocalPoint - lastLinePoint).normalized;

                lastLinePoint = mouseLocalPoint;

                MeshUtils.AddLinePoint(mesh, mouseLocalPoint, 15f);
                lineCanvasRenderer.SetMesh(mesh);

                arrowRectTransform.anchoredPosition = mouseLocalPoint;
                arrowRectTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(forwardVector));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Mouse Released
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

        //GameObject prevLine = 

        GameObject lineObject = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
}*/