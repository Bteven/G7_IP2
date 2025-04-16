using System.Collections;
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

    [Header("Silo Door Settings")]
    public Transform door1;
    public Transform door2;
    public float doorSlideDistance = 1f;
    public float doorAnimationDuration = 1f;

    private Vector3 door1ClosedPos;
    private Vector3 door2ClosedPos;
    private Vector3 door1OpenPos;
    private Vector3 door2OpenPos;

    private bool isLaunching = false;
    public bool UpgradeOneDone { get; private set; }
    public bool UpgradeTwoDone { get; private set; }

    void Start()
    {
        //Save closed positions of doors
        door1ClosedPos = door1.localPosition;
        door2ClosedPos = door2.localPosition;

        //Calculate open positions of doors
        door1OpenPos = door1ClosedPos + Vector3.forward * doorSlideDistance;
        door2OpenPos = door2ClosedPos + Vector3.back * doorSlideDistance;
        
    }

    void Update()
    {
        if (!isPlaced) return; // Don't shoot if the turret is not placed

        FindTarget();
        //FireMissile();

        if (targetEnemy != null && fireTimer <= 0f && !isLaunching)
        {
            StartCoroutine(LaunchSequence());
        }
        else
        {
            fireTimer -= Time.deltaTime;
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
    void DamageAmount(GameObject missile)
    {
        Attack attack = missile.GetComponent<Attack>();

        attack.damage = missileDamage;



    }

    IEnumerator LaunchSequence()
    {
        isLaunching = true;

        yield return StartCoroutine(SlideDoorsTogether(door1OpenPos, door2OpenPos));

        yield return new WaitForSeconds(0.2f);

        FireMissile();

        yield return new WaitForSeconds(0.8f);

        //Close doors
        yield return StartCoroutine(SlideDoorsTogether(door1ClosedPos, door2ClosedPos));

        isLaunching = false;


    }

    IEnumerator SlideDoorsTogether(Vector3 door1Target, Vector3 door2Target)
    {
        Vector3 door1Start = door1.localPosition;
        Vector3 door2Start = door2.localPosition;

        float elapsed = 0f;

        while (elapsed < doorAnimationDuration)
        {
            float t = elapsed / doorAnimationDuration;
            door1.localPosition = Vector3.Lerp(door1Start, door1Target, t);
            door2.localPosition = Vector3.Lerp(door2Start, door2Target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        door1.localPosition = door1Target;
        door2.localPosition = door2Target;
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

        int damageIncrease = 100;


        missileDamage = missileDamage + damageIncrease;

        UpgradeTwoDone = true;
    }

}