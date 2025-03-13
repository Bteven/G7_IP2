using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTurret : BaseTurret
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void TurretRotation()       //overrides base class so it is only calls the turret to attack and not rotate as it dosn't need to
    {

        if (enemyObject != null) // checks of enemy is there before finding direction and rotating towards
        {

            SpinAttack();

        }
    }

    void SpinAttack()
    {


        foreach (GameObject enemy in enemyObjectInRange)
        {
            if (enemy != null)
            { 
            
          
            
            }
      
        }


    }
}
