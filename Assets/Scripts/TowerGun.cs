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
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f); // finds correct rotion vector for the bullet
        var newBullet = Instantiate(bulletPrefab,bulletSpawn.transform.position, Quaternion.LookRotation(newDirection)); // fires a new bullet

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

            targetDir = enemyObject.transform.position - rotationPoint.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            firingSequence();

        }
    }

    public void UpgradeState()
    {
        fireCooldown -= 1.5f;
        towerBody.transform.localScale *= 1.1f;
        transform.localScale *= 1.2f;

    }
}
