using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour
{
    [Header("Tower State")]
    public bool isPlaced = false; // Controls whether the turret is allowed to shoot

    [Header("Missile Settings")]
    public Transform missileSpawnPoint;
    public GameObject missilePrefab;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Targeting Settings")]
    public SphereCollider detectionCollider; // Assign the Sphere Collider in the Inspector
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private Transform targetEnemy;

    void Update()
    {
        if (!isPlaced) return; // Don't shoot if the turret is not placed

        FindTarget();
        FireMissile();

        if (Input.GetKeyDown(KeyCode.W))
        {
            UpgradeState();
        }
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

    void FireMissile()
    {
        if (targetEnemy != null && fireTimer <= 0f)
        {
            Debug.Log("Firing missile at target: " + targetEnemy.name);
            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
            Missile missileScript = missile.GetComponent<Missile>();
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

    private void UpgradeState()
    {
        transform.localScale *= 1.5f;
        fireRate -= 1;
        detectionCollider.radius *= 1.5f;
    }
}