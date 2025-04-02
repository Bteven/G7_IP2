using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{

    public List<GameObject> objectInRange = new List<GameObject>();

    private float slowSpeed = 1.5f;
    private float upSlowSpeed = 1f;
    private bool upgradeEnabled;
    public GameObject Sphere;



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
                obj.enemySpeed = slowSpeed;

                if (upgradeEnabled)
                {
                    obj.enemySpeed = upSlowSpeed;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        foreach (GameObject enemy in objectInRange)
        {
            if (enemy.TryGetComponent<Waypoints>(out Waypoints obj))
            {
                obj.enemySpeed = 3;
            }

     
        }
        if (other.CompareTag("Enemy"))
        {
            objectInRange.Remove(other.gameObject);
        }
    }

    private void UpgradeState()
    {
        transform.localScale *= 1.5f;
        Sphere.transform.localScale *= 1.5f;
        upgradeEnabled = true;
    }

    void CleanEnemyList()
    {
        objectInRange.RemoveAll(enemy => enemy == null); //removes all null entries from the list
    }

}
