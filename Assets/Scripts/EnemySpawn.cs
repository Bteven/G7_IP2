using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    //script for spawning enemies at start of path

    [SerializeField]
    private GameObject enemyPrefab; //i'd change this to array when we have the prefab variants

    private float enemyInterval = 2.75f; //enemies spawn every 1 to 3 seconds (this is just a placeholder until we know exactly how often we want them to spawn)

    void Start()
    {
        //enemyInterval = Random.Range(1f, 3f);
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    //Spawns enemies at point just off the screen at the start of the path every 1 to 3 seconds
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(0f, 0f, 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
