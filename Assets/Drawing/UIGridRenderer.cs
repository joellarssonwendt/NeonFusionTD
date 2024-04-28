using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{
    public Vector2[] points;
    public Vector2Int gridSize = new Vector2Int(1, 1);
    [SerializeField] public float thickness = 10f;
    private int vertexCount = 0;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        vertexCount = 0;

        for (int i = 0; i < points.Length; i += 2)
        {
            if (i + 1 >= points.Length) break;
            DrawLine(points[i], points[i + 1], vh);
            vertexCount += 4;
        }
    }

    private void DrawLine(Vector2 pointA, Vector2 pointB, VertexHelper vh)
    {
        float lineAngle = GetAngleBetweenPoints(pointA, pointB);
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        vertex.position = TranslatedByAngleAndDistance(pointA, lineAngle + Mathf.PI / 2, thickness / 2);
        vh.AddVert(vertex);
        vertex.position = TranslatedByAngleAndDistance(pointA, lineAngle - Mathf.PI / 2, thickness / 2);
        vh.AddVert(vertex);
        vertex.position = TranslatedByAngleAndDistance(pointB, lineAngle + Mathf.PI / 2 * 3, thickness / 2);
        vh.AddVert(vertex);
        vertex.position = TranslatedByAngleAndDistance(pointB, lineAngle - Mathf.PI / 2 * 3, thickness / 2);
        vh.AddVert(vertex);
        vh.AddTriangle(vertexCount, vertexCount + 1, vertexCount + 3);
        vh.AddTriangle(vertexCount + 2, vertexCount + 3, vertexCount + 1);
    }

    private float GetAngleBetweenPoints(Vector2 pointA, Vector2 pointB)
    {
        Vector2 lookAt = pointB - pointA;
        return Mathf.Atan2(lookAt.y, lookAt.x);
    }

    private Vector2 TranslatedByAngleAndDistance(Vector2 point, float angle, float distance)
    {
        Vector2 newPoint = point;
        newPoint.x += Mathf.Cos(angle) * distance;
        newPoint.y += Mathf.Sin(angle) * distance;
        return newPoint;
    }

    // Method to handle mouse input
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            // Convert mouse position to world position
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            // Add the new point to the points array
            System.Array.Resize(ref points, points.Length + 2);
            points[points.Length - 2] = worldPosition;
            points[points.Length - 1] = worldPosition; // You can adjust this to draw lines between points
        }
    }
}