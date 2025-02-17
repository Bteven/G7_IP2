using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" | col.gameObject.tag == "FinishLine") //If bullet hits player the enemy takes damage or if enemy hits player the finish line (the player's) health takes damage
        {
            //need to change to make sure bullet can't damage the FinishLine

            var healthController = col.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damage);
        }
    }
}
