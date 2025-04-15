using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpgradeManager : MonoBehaviour
{

    public GameObject selectedTurret;
    public bool turretIsSelected;
    public GameObject selectedTower;
    public float turretTypeIndicator; // this will be used so the switch case dosn't need to be made twice
    public GameObject upgradeMenu;
    public UpgradePanelMenu upgradePanelMenu;


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

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit raycastHit = hits[i];
                Debug.Log(raycastHit.collider.gameObject.tag);
                switch (raycastHit.collider.gameObject.tag)
                {
                    case "Gun Tower":
                        if (raycastHit.collider.gameObject.GetComponentInChildren<TowerGun>() != null)
                        {
                            raycastHit.collider.gameObject.GetComponentInChildren<TowerGun>().UpgradeState();
                            selectedTower = raycastHit.collider.gameObject;

                            turretTypeIndicator = 0;
                            upgradeMenu.SetActive(true);
                        }
                        break;

                    case "Zone Tower":

                        raycastHit.collider.gameObject.TryGetComponent<ZoneTurret>(out ZoneTurret ZTComponent);
                        selectedTower = raycastHit.collider.gameObject;

                        ZTComponent.UpgradeState();



                        turretTypeIndicator = 1;
                        upgradeMenu.SetActive(true);


                        break;

                    case "Missile Tower":
                        if (raycastHit.collider.gameObject.GetComponentInParent<MissileTower>() != null)
                        {
                            raycastHit.collider.gameObject.GetComponentInParent<MissileTower>().UpgradeState();
                            selectedTower = raycastHit.collider.gameObject;



                            turretTypeIndicator = 2;
                            upgradeMenu.SetActive(true);
                        }
                        break;

                    case "Slow Tower":
                        raycastHit.collider.gameObject.TryGetComponent<SlowTower>(out SlowTower STComponent);
                        STComponent.UpgradeState();



                        turretTypeIndicator = 3;
                        upgradeMenu.SetActive(true);
                        break;

                    default:

                        // issue this stops all menu stuff before button can be pressed will have to find a new way to set false

                        StartCoroutine(turnOffUpgradePanel());

                        break;
                }
            }
        }
    }

    IEnumerator turnOffUpgradePanel()
    {


        foreach (RangeLineFinder range in FindObjectsOfType<RangeLineFinder>())
        {
            range.currentSelectedTurret = false;
        }


        yield return new WaitForSeconds(3);
        upgradeMenu.SetActive(false);
    }



}