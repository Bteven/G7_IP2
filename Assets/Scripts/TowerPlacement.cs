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
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(camray.origin, camray.direction * 1000f, Color.green);
            RaycastHit HitInfo;
            

            if (Physics.Raycast(camray, out HitInfo, 100f, PlacementCollideMask))
            {
                Debug.Log("Hit : " + HitInfo.collider.name);
                CurrentPlacingTower.transform.position = HitInfo.point;

                if (Input.GetMouseButtonDown(0) && HitInfo.collider.gameObject != null)
                {
                    if (!HitInfo.collider.gameObject.CompareTag("CantPlace"))
                    {
                        BoxCollider TowerCollider = CurrentPlacingTower.gameObject.GetComponent<BoxCollider>();
                        TowerCollider.isTrigger = true;

                        Vector3 BoxCenter = CurrentPlacingTower.gameObject.transform.position + TowerCollider.center;
                        Vector3 HalfExtents = TowerCollider.size / 2;

                        if (!Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity, PlacementCheckMask, QueryTriggerInteraction.Ignore))
                        {
                            TowerCollider.isTrigger = false;
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
    }
}
