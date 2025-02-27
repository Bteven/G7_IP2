using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    //script for spawning enemies at start of path

    [SerializeField]
    private GameObject[] enemyPrefabs; //i'd change this to array when we have the prefab variants

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    //Spawns enemies at point just off the screen at the start of the path every 2.75 seconds
    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            float interval = Random.Range(2f, 5f); //Spawns enemies at a random interval between 2 to 5 seconds
            yield return new WaitForSeconds(interval);

            Vector3 spawnPosition = transform.position; //Use spawner position
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; //Selects a random enemy prefab
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
        
    }
}
