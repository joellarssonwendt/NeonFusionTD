using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class MapUI : MonoBehaviour {

    public static MapUI Instance { get; private set; }

    //public event EventHandler OnShow;
    //public event EventHandler OnHide;


    [SerializeField] private Material lineMaterial;

    private RectTransform rectTransform;
    private RectTransform arrowRectTransform;
    private CanvasRenderer lineCanvasRenderer;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;
    private Vector2 lastLinePoint;


    private void Awake() {
      //  Instance = this;

        rectTransform = GetComponent<RectTransform>();
        //arrowRectTransform = transform.Find("Arrow").GetComponent<RectTransform>();
       lineCanvasRenderer = transform.Find("LineUI").GetComponent<CanvasRenderer>();

       // lineCanvasRenderer.SetMaterial(lineMaterial, null);

      //  Hide();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Mouse Down
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out Vector2 lastLinePoint);
            mesh = MeshUtils.CreateMesh(lastLinePoint, lastLinePoint, lastLinePoint, lastLinePoint);
            lineCanvasRenderer.SetMesh(mesh);
            arrowRectTransform.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0)) {
            // Mouse Held Down
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out Vector2 mouseLocalPoint);
            if (Vector2.Distance(lastLinePoint, mouseLocalPoint) > 5f) {
                // Far enough from last point
                Vector2 forwardVector = (mouseLocalPoint - lastLinePoint).normalized;

                lastLinePoint = mouseLocalPoint;

                MeshUtils.AddLinePoint(mesh, mouseLocalPoint, 15f);
                lineCanvasRenderer.SetMesh(mesh);

                arrowRectTransform.anchoredPosition = mouseLocalPoint;
                arrowRectTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(forwardVector));
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            // Mouse Released
        }
    }

   /* public void Show() {
        gameObject.SetActive(true);
        OnShow?.Invoke(this, EventArgs.Empty);
    }

    public void Hide() {
        gameObject.SetActive(false);
        OnHide?.Invoke(this, EventArgs.Empty);
    }

    public void Toggle() {
        if (gameObject.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }*/

}