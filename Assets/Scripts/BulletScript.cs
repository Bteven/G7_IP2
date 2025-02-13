using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [Header("BulletMovement")]

    public Rigidbody bulletBody;   
    public float bulletSpeed;      //speed multiplier

    [Header("BulletDuration")]

    public float bulletTimer;       //current bullet timer  
    public float maxBulletTime;     // final bullet timer


    void Update()
    {
        BulletMove();
        BulletDespawn();

    }
    void BulletDespawn()
    {
        bulletTimer += Time.deltaTime;       // counts up 

        if (bulletTimer > maxBulletTime)     // when reaches final time
        {
            Destroy(gameObject);            // destroys physical bullet

        }
    }
    void BulletMove()
    {
        bulletBody.velocity = transform.forward * bulletSpeed;  //moves bullet foward
    }
}
 
  