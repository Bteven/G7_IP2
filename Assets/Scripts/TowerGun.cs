using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TowerGun : BaseTurret
{
    [Header("Game Objects")]

    public GameObject towerBody;

    [Header("Firing Variables")]

    public bool targeting;  //true is target is in line
    public float fireCooldown; 
    public float currentFireTimer;
    private bool upgradeEnabled;


    [Header("Bullet Variables")]

    public GameObject bulletPrefab;       //  Stores the bullet prefab 
    public GameObject bulletSpawn;


    void Update()
    {
        base.Update();
    }
      
    void Fire()
    {
        if (enemyObject == null) return;

        Vector3 fireDirection = (enemyObject.transform.position - bulletSpawn.transform.position).normalized;

        Quaternion bulletRotation = Quaternion.LookRotation(fireDirection);
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletRotation);

    }
    void firingSequence()
    {

        if (targeting == true)      // checks if the target is being tracked by the gun
        {
            currentFireTimer += Time.deltaTime;      // adds to the timer

            if (currentFireTimer > fireCooldown)        // when cooldown over
            {
                Fire();                     // calls methoud to fire the bullet
                currentFireTimer = 0;       // resets timer
            }
        }
    }
    protected override void TurretRotation()       //overrides base class so it can call firing sequence in the Guns Rotation
    {
        if (enemyObject != null) // checks of enemy is there before finding direction and rotating towards
        {
            Vector3 directionToEnemy = enemyObject.transform.position - rotationPoint.transform.position;
            directionToEnemy.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy.normalized) * Quaternion.Euler(0, 180f, 0);

            towerBody.transform.rotation = Quaternion.RotateTowards(towerBody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            firingSequence();

        }
    }

    public void UpgradeState()
    {
        base.UpgradeState();

        RangeLineFinder rangeFinder = GetComponentInParent<RangeLineFinder>();

        if (rangeFinder != null)
        {
            rangeFinder.currentSelectedTurret = true;
        }

    }
}
