using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float fixedYPosition;
    private Plane dragPlane;

    //private Vector3 lastValidPosition;
    //private bool isValidPlacement = true;

    //private int noPlacementZoneCount = 0; //tracks how many no placement zones the tower is in

    private void Start()
    {
        fixedYPosition = transform.position.y; //Store initial Y position
        //lastValidPosition = transform.position; //Initial valid position

    }


    private void OnMouseDown()
    {
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedYPosition, 0));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }

        //isValidPlacement = (noPlacementZoneCount == 0); //Allow placement when not in any no placement zones
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            transform.position = ray.GetPoint(enter) + offset;
            transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
        }
        //isValidPlacement = (noPlacementZoneCount == 0);

    }

    private void OnMouseUp()
    {
        //if (!isValidPlacement)
        //{
            //transform.position = lastValidPosition; //Snap back to last valid position
        //}
        //else
        //{
            //lastValidPosition = transform.position; //Update valid position
        //}
        
        
    }
}
