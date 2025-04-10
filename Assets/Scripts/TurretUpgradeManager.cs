using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpgradeManager : MonoBehaviour
{

    public GameObject selectedTurret;
    public bool turretIsSelected;
    private GameObject selectedTower;
    public GameObject upgradeMenu;


    // Start is called before the first frame update
    void Start()
    {

        //this.enabled = false;
        turretIsSelected = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (turretIsSelected == true)
        {
            TurretSelectedMenu();

        }

        if (turretIsSelected == false)
        {

            DeselectTurret();

        }

        LookForTower();
        
    }


    void DeselectTurret()
    {
        if (selectedTurret != null)
        {
            RangeLineFinder rangeFinder;
            rangeFinder = selectedTurret.GetComponentInChildren<RangeLineFinder>();
                      
            rangeFinder.currentSelectedTurret = false;

            selectedTurret = null;


        }
    }
    void TurretSelectedMenu()
    {

        if (selectedTurret != null)
        {


            RangeLineFinder rangeFinder;
            rangeFinder = selectedTurret.GetComponentInChildren<RangeLineFinder>();

            rangeFinder.currentSelectedTurret = true;


        }
    }

    private void LookForTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;

            hits = Physics.RaycastAll(ray, Mathf.Infinity);

            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit raycastHit = hits[i];
                Debug.Log(raycastHit.collider.gameObject.tag);
                switch (raycastHit.collider.gameObject.tag)
                {
                    case "Gun Tower":
                        if (raycastHit.collider.gameObject.GetComponentInChildren<TowerGun>() != null)
                        {
                            raycastHit.collider.gameObject.GetComponentInChildren<TowerGun>().UpgradeState();
                                                        
                            upgradeMenu.SetActive(true);
                        }
                        break;

                    case "Zone Tower":

                        raycastHit.collider.gameObject.TryGetComponent<ZoneTurret>(out ZoneTurret ZTComponent);
                        ZTComponent.UpgradeState();
                        upgradeMenu.SetActive(true);

                        break;

                    case "Missile Tower":
                        if (raycastHit.collider.gameObject.GetComponentInParent<MissileTower>() != null)
                        {
                            raycastHit.collider.gameObject.GetComponentInParent<MissileTower>().UpgradeState();
                            upgradeMenu.SetActive(true);
                        }
                        break;

                    case "Slow Tower":
                        raycastHit.collider.gameObject.TryGetComponent<SlowTower>(out SlowTower STComponent);
                        STComponent.UpgradeState();
                        upgradeMenu.SetActive(true);
                        break;

                        default:
                        upgradeMenu.SetActive(false);

                        foreach (RangeLineFinder range in FindObjectsOfType<RangeLineFinder>())
                        {
                            range.currentSelectedTurret = false;
                        }

                        break;


                }
            }
        } 
    }
}
