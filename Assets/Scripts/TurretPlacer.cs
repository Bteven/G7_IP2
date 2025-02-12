using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public GameObject selectedTurretPrefab;
    private GameObject turretPreview;
    private bool isPlacing = false;


    void Update()
    {
        if (isPlacing && turretPreview != null)
        {
            
            Vector3 mousePos = GetMouseWorldPosition();
            turretPreview.transform.position = mousePos;


            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(selectedTurretPrefab, mousePos, Quaternion.identity);
                Destroy(turretPreview);
                isPlacing = false;
            }
        }
    }

    public void SelectTurret(GameObject turretPrefab)
    {
        if (!isPlacing && turretPrefab != null)
        {
            Destroy(turretPreview);
        }

        selectedTurretPrefab = turretPrefab;
        isPlacing = true;
        turretPreview = Instantiate(selectedTurretPrefab);
        turretPrefab.GetComponent<Collider>().enabled = false;
        Color previewColor = turretPreview.GetComponent<Renderer>().material.color;
        previewColor.a = 0.5f;
        turretPreview.GetComponent<Renderer>().material.color = previewColor;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}

    
