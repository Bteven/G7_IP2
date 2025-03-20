 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTurret : BaseTurret
{


    public Animator animator;
    [SerializeField] float damageAmount;


    [Header("Firing Variables")]

    public float fireCooldown;
    public float currentFireTimer;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    protected override void TurretRotation()       //overrides base class so it is only calls the turret to attack and not rotate as it dosn't need to
    {

        if (enemyObject != null)
        {
            firingSequence();
        }


    }

    void firingSequence()           // copyed from firing tower
    {

     
            currentFireTimer += Time.deltaTime;      // adds to the timer

            if (currentFireTimer > fireCooldown)        // when cooldown over
            {
                SpinAttack();                  // calls methoud to fire the bullet
                currentFireTimer = 0;       // resets timer
            }
        
    }

    void SpinAttack()
    {

       

            animator.SetTrigger("Attacking");
            
            foreach (GameObject enemy in enemyObjectInRange)
            {

            HealthController enemyHealth = enemy.GetComponent<HealthController>();
                enemyHealth.TakeDamage(damageAmount);

            }
    }
}
