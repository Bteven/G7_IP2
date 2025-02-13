using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    //script for spawning enemies at start of path

    [SerializeField]
    private GameObject enemyPrefab; //i'd change this to array when we have the prefab variants

    private float enemyInterval = 2.75f; //enemies spawn every 2.75 seconds (this is just a placeholder until we know exactly how often we want them to spawn)

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    //Spawns enemies at point just off the screen at the start of the path every 2.75 seconds
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        Vector3 spawnPosition = transform.position; //Use spawner position
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
