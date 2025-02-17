using System.Reflection;
using UnityEngine;

public class MissileTower : MonoBehaviour
{
    [Header("Missile Settings")]
    public Transform missileSpawnPoint;
    public GameObject missilePrefab;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Targeting Settings")]
    public float detectionRange = 10f;
    private Transform targetEnemy;

    void Update()
    {
        FindTarget();
        FireMissile();
    }

    void FindTarget()
    {
        GameObject[] enemiesInRange = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = detectionRange;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }

    void FireMissile()
    {
        if (targetEnemy != null && fireTimer <= 0f)
        {
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
}