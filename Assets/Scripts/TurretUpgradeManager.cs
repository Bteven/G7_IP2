using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{

    public GameObject selectedTurret;
    public bool turretIsSelected;


    // Start is called before the first frame update
    void Start()
    {

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
}
