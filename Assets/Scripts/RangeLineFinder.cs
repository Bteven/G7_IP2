using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeLineFinder : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] points;
    public SphereCollider SphereCollider;
    [SerializeField] float radius;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        radius = SphereCollider.radius;
        // Initialize the points array to have 4 elements (since you are using 4 points for the square)
        points = new Vector3[4];

        // Now calculate the points
        CalculatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the line every frame
        DrawLine();
    }

    void DrawLine()
    {
        // Set the number of positions for the LineRenderer and apply the points
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    void CalculatePoints()
    {
        // Set the 4 points for the square
        points[0] = new Vector3(radius, 0.5f, 0);  // Right
        points[2] = new Vector3(-radius, 0.5f, 0); // Left
        points[1] = new Vector3(0, 0.5f, radius);  // Top
        points[3] = new Vector3(0, 0.5f, -radius); // Bottom
    }
}
