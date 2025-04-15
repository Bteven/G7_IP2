using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour, UpgradeTowerInterface
{
    public List<GameObject> objectInRange = new List<GameObject>();
    private Dictionary<GameObject, float> originalSpeeds = new Dictionary<GameObject, float>();
    public SphereCollider rangeColider;


    public float slowSpeedMultiplier = 0.5f;


    public bool UpgradeOneDone { get; private set; }
    public bool UpgradeTwoDone { get; private set; }

    private void Update()
    {
        CleanEnemyList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;

            if (!objectInRange.Contains(enemy))
                objectInRange.Add(enemy);

            if (enemy.TryGetComponent<Waypoints>(out Waypoints obj))
            {
                if (!originalSpeeds.ContainsKey(enemy))
                {
                    originalSpeeds[enemy] = obj.enemySpeed;  
                    obj.enemySpeed *= slowSpeedMultiplier;   
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject enemy = other.gameObject;

        if (enemy.CompareTag("Enemy"))
        {
            objectInRange.Remove(enemy);

            if (enemy.TryGetComponent<Waypoints>(out Waypoints obj))
            {
                if (originalSpeeds.ContainsKey(enemy))
                {
                    obj.enemySpeed = originalSpeeds[enemy];  
                    originalSpeeds.Remove(enemy);           
                }
            }
        }
    }

    public void UpgradeState()
    {
        RangeLineFinder rangeFinder = GetComponentInParent<RangeLineFinder>();
        if (rangeFinder != null)
        {
            rangeFinder.currentSelectedTurret = true;
        }
    }

    void CleanEnemyList()
    {
        objectInRange.RemoveAll(enemy => enemy == null);

        
        List<GameObject> exitingColider = new List<GameObject>();
        foreach (var keyAndValue in originalSpeeds)
        {
            if (keyAndValue.Key == null)
                exitingColider.Add(keyAndValue.Key);
        }

        foreach (var key in exitingColider)
        {
            originalSpeeds.Remove(key);
        }
    }

    public void UpgradeOne()
    {
        // this will decrease the cooldown betwean swings

        int rangeIncrease = 3;

        Debug.Log("Hello");
        rangeColider.radius = rangeColider.radius + rangeIncrease;
        UpgradeOneDone = true;

        RangeLineFinder rangeFinder = GetComponentInParent<RangeLineFinder>();
        if (rangeFinder != null)
        {
            rangeFinder.currentSelectedTurret = true;
            rangeFinder.CalculatePoints(); 

        }

    }
    public void UpgradeTwo()
    {
        // this will decrease the cooldown betwean swings

        float speedDecrease = 0.2f;
        Debug.Log("bye");
        slowSpeedMultiplier = slowSpeedMultiplier - speedDecrease;

        UpgradeTwoDone = true;
    }

}
