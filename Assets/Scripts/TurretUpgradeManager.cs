using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpgradeManager : MonoBehaviour
{

    public GameObject selectedTurret;
    public bool turretIsSelected;
    private GameObject selectedTower;


    // Start is called before the first frame update
    void Start()
    {

       // this.enabled = false;
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

            Debug.Log(hits.Length);

            

            


         //   if (Physics.Raycast(ray.origin, ray.direction, Mathf.Infinity))
         //   {

         //       for (int i = 0; i < hits.Count; i++)
         //       {
         //           RaycastHit sortedHits = hits[i];

         //           if (sortedHits.collider.gameObject.tag == ("Tower"))
         //           {
         //               List<MonoBehaviour> list = new List<MonoBehaviour>();
         //               sortedHits.collider.gameObject.TryGetComponent<SlowTower>(out SlowTower STComponent);
         //               sortedHits.collider.gameObject.TryGetComponent<MissileTower>(out MissileTower MTComponent);
         //               sortedHits.collider.gameObject.TryGetComponent<ZoneTurret>(out ZoneTurret ZTComponent);
         //               sortedHits.collider.gameObject.TryGetComponent<TowerGun>(out TowerGun TGComponent);

         //               list.Add(STComponent);
         //               list.Add(MTComponent);
         //               list.Add(ZTComponent);
         //               list.Add(TGComponent);

         //               print("Success");

         //           }
         //       }
         //   }
        } 
    }
}
