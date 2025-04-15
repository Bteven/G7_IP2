using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour, UpgradeTowerInterface
{
    [Header("Tower State")]
    public bool isPlaced = false; // Controls whether the turret is allowed to shoot

    [Header("Missile Settings")]
    public Transform missileSpawnPoint;
    public GameObject missilePrefab;
    public float fireRate = 2f;
    private float fireTimer;
    public float missileDamage;

    [Header("Targeting Settings")]
    public SphereCollider detectionCollider; // Assign the Sphere Collider in the Inspector
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private Transform targetEnemy;

    public bool UpgradeOneDone { get; private set; }
    public bool UpgradeTwoDone { get; private set; }

    void Update()
    {
        if (!isPlaced) return; // Don't shoot if the turret is not placed

        FindTarget();
        FireMissile();
    }

    void FindTarget()
    {
        if (enemiesInRange.Count > 0)
        {
            // Find the closest enemy in range
            Transform closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemiesInRange)
            {
                if (enemy == null) continue; // Skip destroyed enemies

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }

            targetEnemy = closestEnemy;
        }
        else
        {
            targetEnemy = null; // No enemies in range
        }
    }
    void DamageAmount(GameObject missile)
    {
        Attack attack = missile.GetComponent<Attack>();

        attack.damage = missileDamage;



    }
    void FireMissile()
    {
        if (targetEnemy != null && fireTimer <= 0f)
        {
            Debug.Log("Firing missile at target: " + targetEnemy.name);
            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
            Missile missileScript = missile.GetComponent<Missile>();
            DamageAmount(missile);
            if (missileScript != null)
            {
                missileScript.SetTarget(targetEnemy);
            }
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Check if the object is an enemy
        {
            enemiesInRange.Add(other.gameObject); // Add the enemy to the list
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) // Check if the object is an enemy
        {
            enemiesInRange.Remove(other.gameObject); // Remove the enemy from the list
        }
    }

    public void UpgradeState()
    {

        RangeLineFinder rangeFinder = GetComponentInChildren<RangeLineFinder>();
        if (rangeFinder != null)
        {
            rangeFinder.currentSelectedTurret = true;
        }
        else
        {
            Debug.Log("no range finder found");
        }
    }

    public void UpgradeOne()
    {
        // this will decrease the cooldown firing

        float fireCooldownUpgradeValue = 1.5f;

        fireRate = fireRate - fireCooldownUpgradeValue;
        UpgradeOneDone = true;
    }
    public void UpgradeTwo()
    {
        // increase damage


        Debug.Log("Hello");

        int damageIncrease = 30;


        missileDamage = missileDamage + damageIncrease;

        UpgradeTwoDone = true;
    }

}