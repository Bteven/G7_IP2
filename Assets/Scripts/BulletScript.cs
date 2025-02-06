using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject bulletObject;
    public Vector3 bulletSpeed;

 
    // Update is called once per frame
    void Update()
    {
        bulletObject.transform.position = bulletObject.transform.position + bulletSpeed * Time.deltaTime;
    }
}
