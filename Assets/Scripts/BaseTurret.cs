using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{

    // Added headers to orgnise them better in unity inspector they add no function

    [Header("Tower Rotation Variables")]

    public GameObject rotationPoint;
    public float rotationSpeed;
    public Vector3 targetDir;           // stores vector direction of target enemy



    [Header("Enemy Detection")]

    public List<GameObject> enemyObjectInRange = new List<GameObject>();  // Used to store a list of all enemys in range
    public GameObject enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
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

    protected virtual void GunRotation()
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

}
