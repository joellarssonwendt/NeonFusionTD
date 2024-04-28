using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class DrawMesh : MonoBehaviour {


    //[SerializeField] private Transform debugVisual1;
    //[SerializeField] private Transform debugVisual2;


    private Mesh mesh;
    private Vector3 lastMousePosition;
    private List<Vector3> verticesList = new List<Vector3>();
    private bool isSquare = false;

    private void Update() {



        if (Input.GetMouseButtonDown(0)) {
            // Mouse Pressed
            mesh = new Mesh();

            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            transform.position = mouseWorldPosition;

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            verticesList.Clear();
            verticesList.Add(mouseWorldPosition);
            lastMousePosition = mouseWorldPosition;

            vertices[0] = UtilsClass.GetMouseWorldPosition();
            vertices[1] = UtilsClass.GetMouseWorldPosition();
            vertices[2] = UtilsClass.GetMouseWorldPosition();
            vertices[3] = UtilsClass.GetMouseWorldPosition();


            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.MarkDynamic();

            GetComponent<MeshFilter>().mesh = mesh;

            lastMousePosition = UtilsClass.GetMouseWorldPosition();


        }

        if (Input.GetMouseButton(0)) {
            // Mouse held down
            float minDistance = .001f;
            if (Vector3.Distance(UtilsClass.GetMouseWorldPosition(), lastMousePosition) > minDistance)
            {
                Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[mesh.uv.Length + 2];
                int[] triangles = new int[mesh.triangles.Length + 6];

                mesh.vertices.CopyTo(vertices, 0);
                mesh.uv.CopyTo(uv, 0);
                mesh.triangles.CopyTo(triangles, 0);

                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;

                Vector3 mouseForwardVector = (UtilsClass.GetMouseWorldPosition() - lastMousePosition).normalized;
                Vector3 normal2D = new Vector3(0, 0, -1f);
                float lineThickness = 1f;
                float zOffset = -8.3f;
                Vector3 newVertexUp = UtilsClass.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness + new Vector3(0, 0, zOffset);
                Vector3 newVertexDown = UtilsClass.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness + new Vector3(0, 0, zOffset);

                vertices[vIndex2] = newVertexUp;
                vertices[vIndex3] = newVertexDown;

                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;

                int tIndex = triangles.Length - 6;

                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex2;
                triangles[tIndex + 2] = vIndex1;

                triangles[tIndex + 3] = vIndex1;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;

                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;

                    for (int i = 0; i < mesh.vertices.Length; i++) {
                        mesh.vertices[i] = UtilsClass.GetMouseWorldPosition();

                        if (Vector3.Distance(UtilsClass.GetMouseWorldPosition(), lastMousePosition) > minDistance)
                        {
                            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                            verticesList.Add(mouseWorldPosition);
                            lastMousePosition = mouseWorldPosition;

                            if (verticesList.Count == 4)
                            {
                                // Check if the vertices form a square
                                isSquare = CheckIfSquare(verticesList);
                                if (isSquare)
                                {
                                    // Trigger the spell
                                    TriggerSpell();
                                }
                                mesh.vertices = mesh.vertices;
                                mesh.RecalculateBounds();

                                lastMousePosition = UtilsClass.GetMouseWorldPosition();

                            }
                        }
                    }
                } 
            }
        }
    private bool CheckIfSquare(List<Vector3> vertices)
    {
        
        float side1 = Vector3.Distance(vertices[0], vertices[1]);
        float side2 = Vector3.Distance(vertices[1], vertices[2]);
        float side3 = Vector3.Distance(vertices[2], vertices[3]);
        float side4 = Vector3.Distance(vertices[3], vertices[0]);

        return Mathf.Approximately(side1, side2) && Mathf.Approximately(side2, side3) && Mathf.Approximately(side3, side4);
    }
    private void TriggerSpell()
    {
      
        Debug.Log("Spell triggered!");
    }
}