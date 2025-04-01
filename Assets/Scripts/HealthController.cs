using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public float currentHealth;
    public GameObject finishLine;

    public int rewardAmount = 50;

    [SerializeField]
    private float maxHealth;

    //can input code for health bar here if needed


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
            if (gameObject.CompareTag("Enemy")) //When enemies health is 0 they die
            {
                if(CurrencyManager.Instance != null)
                {
                    GameObject spawnManager = GameObject.FindObjectOfType<SpawnManager>().gameObject;
                    if(spawnManager != null)
                    {
                        SpawnManager sm = spawnManager.GetComponent<SpawnManager>();
                        if(sm != null)
                        {
                            sm.AddKillReward(rewardAmount);
                        }
                    }
                }

                Destroy(gameObject);
            }

            if (gameObject.CompareTag("FinishLine")) //When finish line health is 0 it's game over
            {
                Debug.Log("GAME OVER!");

                //Add game over logic
            }
        }
    }
    private void OnTriggerEnter(Collider col) //not ideal to put this here but i'll try move it when we combine the scene
    {
        if (col.gameObject.CompareTag("Bullet")) //Destroys bullet if it hits player and destroys enemy if it hits the finish line
        {
            Destroy(col.gameObject);


            var damageScript = col.gameObject.GetComponent<Attack>();
            var damage = damageScript.damage;


            TakeDamage(damage);

        }
    }

    //can add code for gaining health here
}
