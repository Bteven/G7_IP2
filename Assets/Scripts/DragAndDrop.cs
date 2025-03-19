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

    private Renderer indicatorRenderer;
    private GameObject placementIndicator;

    private void Start()
    {
        fixedYPosition = transform.position.y; //Store initial Y position
        lastValidPosition = transform.position; //Initial valid position

        //Find the placement indicator (circle)
        placementIndicator = transform.Find("PlacementIndicator").gameObject;
        indicatorRenderer = placementIndicator.GetComponent<Renderer>();

        placementIndicator.SetActive(true); //hides circle initially
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedYPosition, 0));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }

        isValidPlacement = false;
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
            isValidPlacement = false;
            indicatorRenderer.material.color = Color.red; //Change circle to red when not placeable
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NoPlacementZone")) // && gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            isValidPlacement = true;
            indicatorRenderer.material.color = Color.green; //Change circle to green when placeable
        }
    }


}
