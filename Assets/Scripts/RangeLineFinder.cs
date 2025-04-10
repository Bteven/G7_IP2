using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RangeLineFinder : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] points;

    public bool currentSelectedTurret;
    public SphereCollider sphereCollider;
    public GameObject rangeIndicator;
    [SerializeField] float radius;
    [SerializeField] float rotationSpeed;
    float numberOfPoints = 36;
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        radius = sphereCollider.radius;
        points = new Vector3[4];
      

        // Now calculate the points
        CalculatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIndicaterActive();
        DrawLine();
        RotateLine();
    }

    public void CheckIndicaterActive()
    {

        if (currentSelectedTurret)
        {
            rangeIndicator.SetActive(true);
        }
        else
        {
            rangeIndicator.SetActive(false);
        }
    
    }   
      void RotateLine()
    {

        lineRenderer.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);


    }

    void DrawLine()
    {
        // populates the line renderer with the calculated points 
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    void CalculatePoints()
    {
        // makes the points into vectors
        points[0] = new Vector3(radius, 0.5f, 0);  // Right
        points[2] = new Vector3(-radius, 0.5f, 0); // Left
        points[1] = new Vector3(0, 0.5f, radius);  // Top
        points[3] = new Vector3(0, 0.5f, -radius); // Bottom
    }
}