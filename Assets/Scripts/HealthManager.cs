using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerHealthManager : MonoBehaviour
{
    public float playerHealth;
    public GameObject finishLine;
    public float maxPlayerHealth;
    public Image HealthBar;
    public Shake cameraShake; //shake script

    //can input code for health bar here if needed
    public void Start()
    {
        playerHealth = maxPlayerHealth;
        HealthBar.fillAmount = maxPlayerHealth / maxPlayerHealth;
    }
    public void Update()
    {
        //make sure to check playerHealth can go over max health
    }

    public void PlayerDamage(float damage)
    {
        
        playerHealth -= damage;
        
        //camera shake
        if (cameraShake != null)
        {
            cameraShake.start = true;
        }

        HealthBar.fillAmount = playerHealth / maxPlayerHealth;

        if (playerHealth <= 0)
        {
            playerHealth = 0;


            Debug.Log("GAME OVER!");
            Time.timeScale = 0f;
        }
    }
      
}
