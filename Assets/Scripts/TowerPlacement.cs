using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask PlacementCheckMask;
    [SerializeField] private LayerMask PlacementCollideMask;
    [SerializeField] private Camera PlayerCamera;

    private GameObject CurrentPlacingTower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CurrentPlacingTower != null)
        {

            Collider towerCollider = CurrentPlacingTower.GetComponent<Collider>();
            towerCollider.enabled = false;

            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(camray.origin, camray.direction * 1000f, Color.green);
            RaycastHit HitInfo;
            

            if (Physics.Raycast(camray, out HitInfo, 100f, PlacementCollideMask))
            {
                Debug.Log("Hit : " + HitInfo.collider.name);
                CurrentPlacingTower.transform.position = HitInfo.point;

                if (Input.GetMouseButtonDown(0))
                {
                    if (!HitInfo.collider.CompareTag("CantPlace"))
                    {

                        Vector3 BoxCenter = CurrentPlacingTower.transform.position + towerCollider.bounds.center - towerCollider.transform.position;
                        Vector3 HalfExtents = towerCollider.bounds.extents;

                        if (!Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity, PlacementCheckMask, QueryTriggerInteraction.Ignore))
                        {
                            towerCollider.enabled = true;
                            CurrentPlacingTower = null;
                            Debug.Log("Tower Placed!");
                        }
                        else
                        {
                            Debug.Log("Blocked by something in PlacementCheckMask");
                        }
                    }
                    else
                    {
                        Debug.Log("Hit object is tagged CantPlace");
                    }
                }
            }

            
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);

        Collider col = CurrentPlacingTower.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }
}
