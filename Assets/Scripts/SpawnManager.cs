﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject spawnPoint;               // enemy spawn point
    public List<GameObject> enemyPrefabs;           //will use this list to spawn enemys in order of ((NORMAL ALIEN, FAST ALIEN, UFOS(TANK))
    public GameObject enemyBoss;                // enemy boss object
    public GameObject waveCompeledPanel;        // wave panel for telling player wave completed
    public TextMeshProUGUI waveCompeledText;


    [Header("Public Varibles")]

    public bool waveCleared;                    // true when a wave is cleared

    public int cooldownForWave;                             // what the cooldown must reach
    public int CooldownPerUnit;                             //how fast enemys spawn after one another
    public int baseSpawnBurstTimer;                     // time until next group spawns

    [Header("Serialize Fields")]

    [SerializeField] int groupDiff;                          //indecates what troops are spawning 0 = normal 1 = fast 2 = tank
    [SerializeField] int waveDiff;                          // how many groups will spawn per wave 
    [SerializeField] int baseGroupSpawnPerWave = 3;         // this plus wave number is how many spawn per wave(wave diff)

    [SerializeField] int defaultGroupSize = 3;              // how many unitys per group changes depending unit level
    [SerializeField] int groupSize;                         // how many units in the current group
    
    [SerializeField] int waveNumber;                        //number of waves that have passed
    [SerializeField] int groupsSent;                        //number of groups sent that wave

    [SerializeField] float currentWaveTimer;                // cooldown until next wave starts

    [SerializeField] int spawnBurstTimer;                    // current time until next group spawns {can be edited in future to change speeds of wave}
    [SerializeField] float currentBurstTime = 0;             // cooldown for how fast the enemy groups spawn

    bool bossSpawned = false;

    public BossScript bossScript;
    public int bossBonus; 

    [Header("Currency")]
    public int totalEnemyKillReward = 0;
    public int totalEnemiesSpawned = 0;
    public int enemiesKilledThisWave = 0;

    // Update is called once per frame
    private void Start()
    {
        waveCleared = false;            
        waveNumber = 0;
        spawnBurstTimer = baseSpawnBurstTimer;
       // initilising that wave isnt over and on wave one also setting spawn group time equal to the default

        WaveDiffCalculator(); // inital wave managed

    }
    void Update()
    {
        if (waveNumber < 8)
        {
            WaveManager();      // makes sure waves are being processed
        }
        else if (bossSpawned == false)
        {

            spawnBoss();

        }


    }
    void GroupDiffCalculator()
    {
        
        if (waveNumber == 5 && groupsSent >= 3 || waveNumber == 6 && groupsSent >= 2 || waveNumber == 7 && groupsSent >= 3)
        {
            groupDiff = 2;
        }
        else if (waveNumber == 2 && groupsSent >= 2 || waveNumber == 3 && groupsSent >= 1 || waveNumber == 4 && groupsSent >= 2 || waveNumber == 7 && groupsSent >= 0)
        {
            groupDiff = 1;
        }
        else
        {
            groupDiff = 0;
        }


    }
    void spawnBoss()
    {

        CurrencyManager.Instance.AddMoney(bossBonus);

        GameObject newEnemy = Instantiate(enemyBoss, spawnPoint.transform.position, Quaternion.identity);
        // spawns an enemy depending the group dificulty set earlyer and will ether spawn normal,fast,tank
        bossScript.getUFO(newEnemy);
        if (newEnemy.GetComponent<HealthController>() == null)
        {
            newEnemy.AddComponent<HealthController>();
        }
        bossSpawned = true; 
    }
    void WaveManager()
    {
        if (groupsSent >= waveDiff && !waveCleared)          // checks if groups sent is the correct amount
        {
                   
            waveCleared = true;             // sets wave to be cleared 
            StartCoroutine(WaveCooldown());                 // starts cooldown until next wave

            if (CurrencyManager.Instance != null)
            {
                StartCoroutine(DelayedRewardDistribution());
            }
        }
        else
        {
           
            GroupDiffCalculator();         // checks unit type to be sent
            GroupSpawnCoolDown();          // after cooldown spawns the group
        }
    }

    IEnumerator DelayedRewardDistribution()
    {
        yield return new WaitForSeconds(3f);

        CurrencyManager.Instance.AddMoney(totalEnemyKillReward);

        if (enemiesKilledThisWave == totalEnemiesSpawned)
        {
            int bonusAmount = 250;
            CurrencyManager.Instance.AddMoney(bonusAmount);
            Debug.Log($"[BONUS] All enemies killed! Bonus money awarded: {bonusAmount}");
        }
        else
        {
            Debug.Log("[BONUS] Not all enemies were killed, no bonuses were given.");
        }

        totalEnemyKillReward = 0;
        totalEnemiesSpawned = 0;
        enemiesKilledThisWave = 0;
    }

    IEnumerator WaveCooldown()
    {
        currentWaveTimer += Time.deltaTime;
            
        // coolsdown then sets wave cleared to false starting the next wave


         
            currentWaveTimer = 0;
            groupsSent = 0;
            waveNumber++;
            WaveDiffCalculator();
          yield return new WaitForSeconds(cooldownForWave);
        
    }
        void WaveDiffCalculator()
        {
        waveCleared = false;
        waveDiff = (waveNumber + baseGroupSpawnPerWave);
            // calcs how many groups will spawn
        }
        void GroupSpawnCoolDown()
        {
            currentBurstTime += Time.deltaTime;

        // cooldown then starts the courotine to spawn the group after cooldown
            if (currentBurstTime > spawnBurstTimer)
            {
            groupSize = defaultGroupSize - groupDiff;
            spawnBurstTimer = baseSpawnBurstTimer + CooldownPerUnit * groupSize;
            // calculates the group size depending the current units in the group
            // also sets the spawn busrt timer realtive to the units being spawned and how long they take to spawn


            StartCoroutine(GroupSpawn());                
                groupsSent++;
                currentBurstTime = 0;
            }
        }
    IEnumerator GroupSpawn()
    {
        // This will run for each enemy in the group and only spawns an enemy after waited a cooldown so they dont all spawn in same time
        // works along side the group spawn timer 
        for (int i = 0; i < groupSize; i++)
        {
            SpawnEnemy();  // Spawn one enemy

            yield return new WaitForSeconds(CooldownPerUnit);  // has to wait before looping again
        }
       
    }

    void SpawnEnemy()
    {
          GameObject newEnemy = Instantiate(enemyPrefabs[groupDiff], spawnPoint.transform.position, Quaternion.identity);
        // spawns an enemy depending the group dificulty set earlyer and will ether spawn normal,fast,tank

        if(newEnemy.GetComponent<HealthController>() == null)
        {
            newEnemy.AddComponent<HealthController>();
        }
    }

    public void AddKillReward(int amount)
    {
        totalEnemyKillReward += amount;
        enemiesKilledThisWave++;
    }
}
