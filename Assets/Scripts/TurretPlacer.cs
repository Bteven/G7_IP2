using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public GameObject selectedTurretPrefab; // Current selected turret prefab
    private GameObject turretPreview; // Placement preview of turret
    private bool isPlacing = false; // boolean tracking if player is placing turret 
    private bool isUpgrading = false;

    [System.Serializable]
    public class TurretData
    {
        public string turretName;
        public GameObject turretPrefab;
        public int cost;
        public int upgradeCost;
    }

    public TurretData[] turrets;
    public List<GameObject> SpawnedTurrets;
    private int currentTurretCost;

    void Update()
    {
        PlaceTurret();
    }

    public void SelectTurret(int index)
    {
        if (index < 0 || index >= turrets.Length)
        {
            return;
        }

        int turretCost = turrets[index].cost;

        if(CurrencyManager.Instance == null || CurrencyManager.Instance.CurrentCurrency < turretCost)
        {
            Debug.Log("Not enough money to select this turret.");
            return;
        }

        selectedTurretPrefab = turrets[index].turretPrefab;
        currentTurretCost = turrets[index].cost;
        isPlacing = true;

        // Destroy any existing preview and replace with the newly selected one
        if (turretPreview != null)
        {
            Destroy(turretPreview);
        }

        turretPreview = Instantiate(selectedTurretPrefab);
        turretPreview.GetComponent<Collider>().enabled = false;

        // Set the preview turret's isPlaced state to false
        MissileTower previewMissileTowerScript = turretPreview.GetComponent<MissileTower>();
        if (previewMissileTowerScript != null)
        {
            previewMissileTowerScript.isPlaced = false;
        }
    }

    private Vector3 GetMouseWorldPosition(out bool isOnGround)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            isOnGround = true;
            return hit.point; 
        }

        isOnGround = false;
        return Vector3.zero;
    }

    private void PlaceTurret()
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
                if (CurrencyManager.Instance.SpendMoney(currentTurretCost))
                {
                    // Place the turret
                    GameObject newTurret = Instantiate(selectedTurretPrefab, mousePos, Quaternion.identity);
                    SpawnedTurrets.Add(newTurret);

                    // Set the turret's isPlaced state to true
                    MissileTower missileTowerScript = newTurret.GetComponent<MissileTower>();
                    if (missileTowerScript != null)
                    {
                        missileTowerScript.isPlaced = true;
                    }

                    Destroy(turretPreview);
                    isPlacing = false;
                }
            }
            else if (Input.GetMouseButtonDown(0) && !isOnGround)
            {
                Debug.Log("Invalid placement: Turret must be placed on the ground.");
            }
        }
    }
}
