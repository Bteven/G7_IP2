using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [Header("Tower State")]
    public bool isPlaced = false; // Turret is not allowed to shoot until it's placed

    [Header("Tower Rotation Variables")]
    public GameObject rotationPoint;
    public float rotationSpeed;
    public Vector3 targetDir;

    [Header("Enemy Detection")]
    public List<GameObject> enemyObjectInRange = new List<GameObject>();
    public GameObject enemyObject;

    protected virtual void Update()
    {
        if (!isPlaced) return; // Don't execute any shooting or targeting logic if the turret is not placed

        FindEnemy();
        TurretRotation();
        CleanEnemyList();
    }

    void FindEnemy()
    {
        if (enemyObjectInRange.Count > 0) // Check if list has enemies
        {
            enemyObject = enemyObjectInRange[0]; // Assign the first enemy (will be latest added which is most recent colision)
        }
        else
        {
            enemyObject = null; // sets enemyobject empty if none in range
        }
    }

    protected virtual void TurretRotation()
    {
        if (enemyObject != null) // checks of enemy is there before finding direction and rotating towards
        {

            targetDir = enemyObject.transform.position - rotationPoint.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
    

        }
    }
 

    //Method to remove destroyed enemies
    void CleanEnemyList()
    {
        enemyObjectInRange.RemoveAll(enemy => enemy == null); //removes all null entries from the list
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Finds Objects with enemy tag and adds to list when in the coliders range
        {
            enemyObjectInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) // Finds Objects with enemy tag and Removes from list when out of coliders range
        {
            enemyObjectInRange.Remove(other.gameObject);
        }
    }

    protected virtual void UpgradeState()
    {
        transform.localScale *= 1.5f;
    }
}
