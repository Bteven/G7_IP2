using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BaseHealthController : MonoBehaviour
{

    public PlayerHealthManager manager;

   private void OnTriggerEnter(Collider col) //not ideal to put this here but i'll try move it when we combine the scene
    {
        if (col.gameObject.CompareTag("Enemy")) //Destroys bullet if it hits player and destroys enemy if it hits the finish line
        {
            Destroy(col.gameObject);

           
            var damageScript = col.gameObject.GetComponent<Attack>();
            var damage = damageScript.damage;


            manager.PlayerDamage(damage);

        }
    }


    //can add code for gaining health here

}
