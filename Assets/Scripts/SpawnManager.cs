using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject spawnPoint;
    public List<GameObject> enemyPrefabs;           //will use this list to spawn enemys in order of ((NORMAL ALIEN, FAST ALIEN, UFOS(TANK))
    public GameObject enemyBoss;
    public GameObject waveCompeledPanel;
    public TextMeshProUGUI waveCompeledText;


    public bool waveCleared;

    [SerializeField] int groupDiff;       //indecates what troops are spawning 0 = normal 1 = fast 2 = tank
    [SerializeField] int waveDiff;        // how many groups will spawn per wave 
    [SerializeField] int baseGroupSpawnPerWave = 3;

    [SerializeField] int defaultGroupSize = 3;
    [SerializeField] int groupSize;
    
    [SerializeField] int waveNumber;
    [SerializeField] int groupsSent;

    [SerializeField] float currentWaveTimer;
    [SerializeField] int cooldownForWave;

    [SerializeField] int CooldownPerUnit;      //how fast enemys spawn after one another

    [SerializeField] int baseSpawnBurstTimer = 10;
    [SerializeField] int spawnBurstTimer; // how fast the enemy groups spawn
    [SerializeField] float currentBurstTime = 0;

    // Update is called once per frame
    private void Start()
    {
        waveCleared = false;
        waveNumber = 1;
        spawnBurstTimer = baseSpawnBurstTimer;

        WaveDiffCalculator(); // inital wave managed

    }
    void Update()
    {
        WaveManager();
    }
    void GroupDiffCalculator()
    {
        //orignal format left here so i can think clearer of what this does 
        //if (waveNumber <= 2)
        //{ 
        //    groupDiff = 0;
        //}
        //else if (waveNumber == 2 && groupsSent >= 2 || waveNumber == 3 && groupsSent >= 1 || waveNumber == 4 && groupsSent >= 2)
        //{
        //    groupDiff = 1;
        //}
        //else if (waveNumber == 5 && groupsSent >= 3 || waveNumber == 6 && groupsSent >= 2)
        //{
        //    groupDiff = 2;
        //}


        if (waveNumber == 5 && groupsSent >= 3 || waveNumber == 6 && groupsSent >= 2)
        {
            groupDiff = 2;
        }
        else if (waveNumber == 2 && groupsSent >= 2 || waveNumber == 3 && groupsSent >= 1 || waveNumber == 4 && groupsSent >= 2)
        {
            groupDiff = 1;
        }
        else
        {
            groupDiff = 0;
        }



    }
    void WaveManager()
    {
        if (groupsSent > waveDiff)
        {
            Debug.Log("WAVE OVER");
            waveCleared = true;
            WaveCooldown();
        }
        else
        {
                GroupDiffCalculator();
                GroupSpawnCoolDown();          
        }       
    }
    void WaveCooldown()
    {
        currentWaveTimer += Time.deltaTime;
            

        if (currentWaveTimer > cooldownForWave)
        {
            waveCleared = false;
            currentWaveTimer = 0;
            groupsSent = 0;
            waveNumber++;
            WaveDiffCalculator();
        }
    }
        void WaveDiffCalculator()
        {
            waveDiff = (waveNumber + baseGroupSpawnPerWave);
            // calcs how many groups will spawn
        }
        void GroupSpawnCoolDown()
        {
            currentBurstTime += Time.deltaTime;

            if (currentBurstTime > spawnBurstTimer)
            {
            groupSize = defaultGroupSize - groupDiff;
            spawnBurstTimer = baseSpawnBurstTimer + CooldownPerUnit * groupSize;

            StartCoroutine(GroupSpawn());                
                groupsSent++;
                currentBurstTime = 0;
            }
        }
    IEnumerator GroupSpawn()
    {
        // This will run for each enemy in the group
        for (int i = 0; i < groupSize; i++)
        {
            SpawnEnemy();  // Spawn one enemy
            Debug.Log("Enemy " + i + " spawned");
            yield return new WaitForSeconds(CooldownPerUnit);  // Wait before spawning the next enemy
        }
    }
    void SpawnEnemy()
        {
            Debug.Log("EnemySpawned");
            GameObject newEnemy = Instantiate(enemyPrefabs[groupDiff], spawnPoint.transform.position, Quaternion.identity);
        }
    }
