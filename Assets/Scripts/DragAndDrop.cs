using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float fixedYPosition;
    private Plane dragPlane;

    private Vector3 lastValidPosition;
    private bool isValidPlacement = true;

    private GameObject placementIndicator;

    private int noPlacementZoneCount = 0; //tracks how many no placement zones the tower is in

    private void Start()
    {
        fixedYPosition = transform.position.y; //Store initial Y position
        lastValidPosition = transform.position; //Initial valid position

        //Find the placement indicator (circle)
        placementIndicator = transform.Find("PlacementIndicator").gameObject;

        placementIndicator.SetActive(false); //hides circle initially

        //ValidateInitialPlacement();
    }

    //private void ValidateInitialPlacement()
    //{
        //Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); //Check for nearby colliders
        //noPlacementZoneCount = 0;

        //foreach (Collider collider in colliders)
        //{
            //if (collider.CompareTag("NoPlacementZone"))
            //{
                //noPlacementZoneCount++;
            //}
        //}

       // isValidPlacement = (noPlacementZoneCount == 0);

       // if (!isValidPlacement)
       // {
           // FindValidStartPosition();
        //}
        //else
        //{
            //lastValidPosition = transform.position;
        //}


    //}

    //private void FindValidStartPosition()
    //{
        //Vector3 newPosition = transform.position;

        //while (!isValidPlacement)
        //{
            //newPosition.y += 1f;
            //Collider[] colliders = Physics.OverlapSphere(newPosition, 0.5f);
            //noPlacementZoneCount = 0;

            //foreach (Collider collider in colliders)
            //{
                //if (collider.CompareTag("NoPlacementZone"))
                //{
                    //noPlacementZoneCount++;
                //}
            //}

            //isValidPlacement = (noPlacementZoneCount == 0);
        //}

        //transform.position = newPosition;
        //lastValidPosition = newPosition;
    //}
    private void OnMouseDown()
    {
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedYPosition, 0));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }

        //isValidPlacement = (noPlacementZoneCount == 0); //Allow placement when not in any no placement zones
        placementIndicator.SetActive(true); //show indicator when dragging tower
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            transform.position = ray.GetPoint(enter) + offset;
            transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
        }
        isValidPlacement = (noPlacementZoneCount == 0);

    }

    private void OnMouseUp()
    {
        if (!isValidPlacement)
        {
            transform.position = lastValidPosition; //Snap back to last valid position
        }
        else
        {
            lastValidPosition = transform.position; //Update valid position
        }

        placementIndicator.SetActive(false); //Hide indicator after placing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NoPlacementZone")) // && gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            Debug.Log($"Entered NoPlacementZone: {other.gameObject.name}");
            noPlacementZoneCount++;
            isValidPlacement = false;
            Debug.Log($"noPlacementZoneCount: {noPlacementZoneCount}, isValidPlacement: {isValidPlacement}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NoPlacementZone")) // && gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            Debug.Log($"Exited NoPlacementZone: {other.gameObject.name}");
            noPlacementZoneCount = Mathf.Max(0, noPlacementZoneCount - 1);
            isValidPlacement = (noPlacementZoneCount == 0);
            Debug.Log($"noPlacementZoneCount: {noPlacementZoneCount}, isValidPlacement: {isValidPlacement}");
            
        }
    }
}
