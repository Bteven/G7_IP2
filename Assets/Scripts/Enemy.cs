using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform pointA, pointB, pointC, pointD; //control points;

    [SerializeField]
    private Transform[] routes;

    private bool coroutineAllowed;

    private int routeToGo;

    private Transform enemyPos;

    private float tcount;

    private float speed;

    private Vector3 gizmosPos;

    public GameObject Parent;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() //draw enemy path in viewport
    {
       for(float t = 0; t < 1; t += 0.05f)
        {
            gizmosPos = Mathf.Pow(1 - t, 3) * pointA.position + 3 * Mathf.Pow(1 - t, 2) * t * pointB.position +
                3 * Mathf.Pow(1 - t, 1) * Mathf.Pow(t, 2) * pointC.position + Mathf.Pow(t, 3) * pointD.position;

            Gizmos.color = Color.green;

            Gizmos.DrawSphere(gizmosPos, 0.1f);

            //generates a visual representation of the curve

        }

        Gizmos.DrawLine(pointA.position, pointB.position);
        Gizmos.DrawLine(pointB.position, pointC.position);
        Gizmos.DrawLine(pointC.position, pointD.position); // draws lines between control points
    }

    private IEnumerator EnemyRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        return null;

    }
}
