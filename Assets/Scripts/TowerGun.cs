using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TowerGun : BaseTurret, UpgradeTowerInterface
{
    [Header("Game Objects")]

    public GameObject towerBody;
    public SphereCollider rangeColider;


    [Header("Firing Variables")]

    public bool targeting;  //true is target is in line
    public float fireCooldown; 
    public float currentFireTimer;
    private bool upgradeEnabled;


    [Header("Bullet Variables")]

    public GameObject bulletPrefab;       //  Stores the bullet prefab 
    public GameObject bulletSpawn;

    private SoundManager soundManager;

    public bool UpgradeOneDone { get; private set; }
    public bool UpgradeTwoDone { get; private set; }

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


                soundManager = FindObjectOfType<SoundManager>();
                soundManager.PlayLazerFireSound(this.transform.position);

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

    public void UpgradeOne()
    {
        // this will decrease the cooldown betwean swings

        int fireCooldownUpgradeValue = 2;

        Debug.Log("Hello");
        fireCooldown = fireCooldown - fireCooldownUpgradeValue;
        UpgradeOneDone = true;
    }
    public void UpgradeTwo()
    {
        // this will decrease the cooldown betwean swings

        int rangeIncrease = 5;

        Debug.Log("Hello");
        rangeColider.radius = rangeColider.radius + rangeIncrease;
        UpgradeTwoDone = true;

        RangeLineFinder rangeFinder = GetComponentInParent<RangeLineFinder>();
        if (rangeFinder != null)
        {
            rangeFinder.currentSelectedTurret = true;
            rangeFinder.CalculatePoints();

        }

    }
}
