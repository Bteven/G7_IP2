using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public float currentHealth;

    [SerializeField]
    private float maxHealth;

    //can input code for health bar here if needed

    public UnityEvent Death;

    public void TakeDamage(float damage)
    {
        if (currentHealth == 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            Death.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) //not ideal to put this here but i'll try move it when we combine the scene
    {
        if (other.gameObject.CompareTag("Bullet") | other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        
    }

    
    //can add code for gaining health here

}
