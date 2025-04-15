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
    [SerializeField] float rotationIndicatorSpeed;
    int numberOfPoints = 20;
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        radius = sphereCollider.radius;



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

        lineRenderer.transform.Rotate(Vector3.up, rotationIndicatorSpeed * Time.deltaTime);


    }

    void DrawLine()
    {
        // populates the line renderer with the calculated points 
       
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    public void CalculatePoints()
    {

        radius = sphereCollider.radius;

        points = new Vector3[numberOfPoints];


        for (int i = 0; i <= numberOfPoints; i++)
        {

            // this just uses how to find coordinates of a circle and varys the angle keeping it realitive to pi keeping a circle formation
            // tje (float) stops the decimal from being neglected as it would be if it was a intger

            float angle = (float)i / numberOfPoints * 3.14f * 2;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            points[i] = new Vector3(x, 0.5f, z);


        }
    }
}