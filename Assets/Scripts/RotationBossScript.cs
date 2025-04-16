using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBossScript : MonoBehaviour
{
     
    private float UFOrotationSpeed = 25;
    public GameObject bossGameObject;

    // Update is called once per frame
    void Update()
    {
        RotateUFO();
    }

    void RotateUFO()
    {

        bossGameObject.transform.Rotate(Vector3.up, UFOrotationSpeed * Time.deltaTime);


    }

}
