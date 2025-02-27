using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This is just to test that the enemy spawner works and will be removed later on
public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] public float enemySpeed;
    [SerializeField] private float checkDistance = 0.05f;
    [SerializeField] private GameObject parent;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform targetWaypoint;
    private int currentWaypointIndex = 0;

    void Start()
    {
        targetWaypoint = waypoints[0];
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, enemySpeed * Time.deltaTime);

        Vector3 direction = targetWaypoint.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, targetWaypoint.position) < checkDistance)
        {
            targetWaypoint = GetNextWaypoint();
        }

    }


    private Transform GetNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            Destroy(parent);
            return null;
        }

        return waypoints[currentWaypointIndex];

    }
}
