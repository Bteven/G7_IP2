using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZoneTurret : BaseTurret, UpgradeTowerInterface
{
    public Animator animator;
    [SerializeField] float damageAmount;


    [Header("Animation Variables")]

    public bool isSpinning;
    float attackAnimationTime = 1.5f;

    [Header("Firing Variables")]



    public float fireCooldown;
    public float currentFireTimer;

    private SoundManager soundManager;


    public bool UpgradeOneDone { get; private set; }
    public bool UpgradeTwoDone { get; private set; }




    void Start()
    {
        currentFireTimer = fireCooldown;
        isSpinning = false;
         

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    protected override void TurretRotation()       //overrides base class so it is only calls the turret to attack and not rotate as it dosn't need to
    {

        if (enemyObject != null) // checks of enemy is there before finding direction and rotating towards
        {

            firingSequence();

        }
  
    }

    void firingSequence()           // copied from firing tower
    {


        currentFireTimer += Time.deltaTime;      // adds to the timer

        if (currentFireTimer > fireCooldown)        // when cooldown over
        {
            
            StartCoroutine(SpinAttack());                // calls ENumeratort to start spin attack animation 
            currentFireTimer = 0;       // resets timer
        }
      

    }
    IEnumerator SpinAttack()
    {
        isSpinning = true;         
        animator.SetTrigger("Attacking");           // starts the animation

        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayZoneAttackSound(this.transform.position);

        if (enemyObjectInRange.Count == 0)  //if no more enemys in range 
        {
            isSpinning = false;
            animator.SetTrigger("Idle"); 
            yield break;    // breaks the IEneum to avoid error
        }



        yield return new WaitForSeconds(attackAnimationTime);
        // dammage will be done in methoud called from animation event


        ApplySpinDamage();
        animator.SetTrigger("Idle");

        yield return new WaitForSeconds(attackAnimationTime);
        isSpinning = false;

    }

    //will run of of the animation to allow damage to be done during animation
    public void ApplySpinDamage()
    {
        foreach (GameObject enemy in enemyObjectInRange)
        {
            if (enemy != null)
            {
                HealthController enemyHealth = enemy.GetComponent<HealthController>();
                if (enemyHealth != null)
                {
                   
                    enemyHealth.TakeDamage(damageAmount);
                }
            }
        }
    }

    public void UpgradeState()
    {
        base.UpgradeState();
        // fireCooldown -= 2;
       // damageAmount += 5;
    }

    public void UpgradeOne()
    {
        // this will decrease the cooldown betwean swings

        float fireCooldownUpgradeValue = 2.5f;

        Debug.Log("Hello");
        fireCooldown = fireCooldown - fireCooldownUpgradeValue;
        UpgradeOneDone = true;
}
    public void UpgradeTwo()
    {
        // this will decrease the cooldown betwean swings

        int damageIncrease = 20;
        Debug.Log("bye");
        damageAmount = damageAmount + damageIncrease;

        UpgradeTwoDone = true;
    }

}


