using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{

    public List<GameObject> objectInRange = new List<GameObject>();
   

  

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
                obj.enemySpeed = 1.5f;
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

   
}
