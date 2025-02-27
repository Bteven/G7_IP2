using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public GameObject selectedTurretPrefab; // Current selected turret prefab
    private GameObject turretPreview; //Placement preview of turret
    private bool isPlacing = false; // boolean tracking if player is placing turret 

    [System.Serializable]
    public class TurretData
    {
        public string turretName;
        public GameObject turretPrefab;
        public int cost;
    }

    public TurretData[] turrets;
    private int currentTurretCost;

    void Update()
    {
        if (isPlacing && turretPreview != null)
        {
            // Updates turret preview to mouse position and makes preview transparent
            bool isOnGround;
            Vector3 mousePos = GetMouseWorldPosition(out isOnGround);
            turretPreview.transform.position = mousePos;

            Color previewColor = turretPreview.GetComponent<Renderer>().material.color;
            previewColor.a = isOnGround ? 0.5f : 0.2f;
            turretPreview.GetComponent<Renderer>().material.color = previewColor;

            if (Input.GetMouseButtonDown(0) && isOnGround)
            {
                //if (CurrencyManager.Instance.SpendMoney(currentTurretCost))
                //{
                    //Checks if tower is being placed on ground layer
                    Instantiate(selectedTurretPrefab, mousePos, Quaternion.identity);
                    Destroy(turretPreview);
                    isPlacing = false;
               // }
            }
            else if (Input.GetMouseButtonDown(0) && !isOnGround)
            {
                Debug.Log("Invalid placement: Turret must be placed on the ground."); // Debug will be replaced by ingame message later
            }
        }
    }

    public void SelectTurret(int index)
    {
        if(index < 0 || index >= turrets.Length)
        {
            return;
        }

        selectedTurretPrefab = turrets[index].turretPrefab;
        currentTurretCost = turrets[index].cost;
        isPlacing = true;

        //Destorys any existing preview and replaces with the newly selected one and disables the preview colider to avoid unwanted interaction
        if (turretPreview != null)
        {
            Destroy(turretPreview);
        }

        turretPreview = Instantiate(selectedTurretPrefab);
        turretPreview.GetComponent<Collider>().enabled = false;
    }

    private Vector3 GetMouseWorldPosition(out bool isOnGround)
    {
        // Checks to see if mouse is pointing to ground if not, returns false
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            isOnGround = true;
            return hit.point;
        }

        isOnGround = false;
        return Vector3.zero;
    }


   
}


