using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" | collision.gameObject.tag == "FinishLine")
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damage);
        }
    }
}
