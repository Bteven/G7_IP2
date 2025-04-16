using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public HealthController controller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.currentHealth <= 0)
        {
            //call game win panel
        }
    }
}
