using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float fixedYPosition;
    private Plane dragPlane;

    private void Start()
    {
        fixedYPosition = transform.position.y; //Store initial Y position
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedYPosition, 0));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPosition = ray.GetPoint(enter) + offset;
            transform.position = new Vector3(targetPosition.x, fixedYPosition, targetPosition.z);
        }
    }
}
