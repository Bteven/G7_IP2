using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public float damage;

    private SoundManager soundManager;
 
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" | col.gameObject.tag == "FinishLine") //If bullet hits player the enemy takes damage or if enemy hits player the finish line (the player's) health takes damage
        {
            // No longer handles removing players Health now done in "PlayerHealthManager"
            
            if (this.gameObject.name == "Missile")
            {

                soundManager = FindObjectOfType<SoundManager>();
                soundManager.StopMissileSound();
               

            }

        }
    }
}
