using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{

    public List<GameObject> objectInRange = new List<GameObject>();

    private float slowSpeed = 1.5f;
    public GameObject Sphere;
    private float normalSpeed;



    private void Update()
    {
        CleanEnemyList();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            objectInRange.Add(other.gameObject);
        }

        foreach(GameObject enemy in objectInRange)
        {
            if (enemy.TryGetComponent<Waypoints>(out Waypoints obj))
            {
                normalSpeed = obj.enemySpeed;
                obj.enemySpeed = slowSpeed;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        foreach (GameObject enemy in objectInRange)
        {
            if (enemy.TryGetComponent<Waypoints>(out Waypoints obj))
            {
                obj.enemySpeed = normalSpeed;
            }

     
        }
        if (other.CompareTag("Enemy"))
        {
            objectInRange.Remove(other.gameObject);
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
        objectInRange.RemoveAll(enemy => enemy == null); //removes all null entries from the list
    }

}
