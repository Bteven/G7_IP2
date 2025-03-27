using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class PlayerHealthManager : MonoBehaviour
{
    public float playerHealth;
    public GameObject finishLine;
    public float maxPlayerHealth;

    //can input code for health bar here if needed
    public void Start()
    {
        playerHealth = maxPlayerHealth;
    }
    public void Update()
    {
        //make sure to check playerHealth can go over max health
    }

    public void PlayerDamage(float damage)
    {
        
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            playerHealth = 0;


            Debug.Log("GAME OVER!");
            Time.timeScale = 0f;
        }
    }
      
}
