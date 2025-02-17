using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TowerGun : MonoBehaviour
{

    // Added headers to orgnise them better in unity inspector they add no function

    [Header("Tower Rotation Variables")]

    public GameObject rotationPoint;
    public float rotationSpeed;
    public Vector3 targetDir;           // stores vector direction of target enemy

    [Header("Bullet Variables")]

    public GameObject bulletPrefab;       //  Stores the bullet prefab 
    public GameObject bulletSpawn;          
       
    [Header("Enemy Detection")]

    public List<GameObject> enemyObjectInRange = new List<GameObject>();  // Used to store a list of all enemys in range
    public GameObject enemyObject;

    [Header("Firing Variables")]

    public bool targeting;  //true is target is in line
    public float fireCooldown; 
    public float currentFireTimer;


    void Update()
    {
        FindEnemy();
        GunRotation();

        CleanEnemyList(); // Method to remove enemies from in range list if they are destroyed while in range

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
    void Fire()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f); // finds correct rotion vector for the bullet
        var newBullet = Instantiate(bulletPrefab,bulletSpawn.transform.position, Quaternion.LookRotation(newDirection)); // fires a new bullet

    }
    

    void GunRotation()
    {
        if (enemyObject != null) // checks of enemy is there before finding direction and rotating towards
        {
            
            targetDir = enemyObject.transform.position - rotationPoint.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward,targetDir,rotationSpeed * Time.deltaTime,0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            firingSequence();

        }
    }
    void firingSequence()
    {

        if (targeting == true)      // checks if the target is being tracked by the gun
        {
            currentFireTimer += Time.deltaTime;      // adds to the timer

            if (currentFireTimer > fireCooldown)        // when cooldown over
            {
                Fire();                     // calls methoud to fire the bullet
                currentFireTimer = 0;       // resets timer
            }
        }
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

    //Method to remove destroyed enemies
    void CleanEnemyList()
    {
        enemyObjectInRange.RemoveAll(enemy => enemy == null); //removes all null entries from the list
    }
}
