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

    private Vector3 enemyPos;

    private float tcount;

    private Vector3 gizmosPos;

    public float speed;



  


    void Start()
    {
        tcount = 0f;
        routeToGo = 0;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (coroutineAllowed)
        {
            StartCoroutine(EnemyRoute(routeToGo));
        }

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
        Vector3 p3 = routes[routeNumber].GetChild(3).position; //gets the position of each point in the routes array

        for (tcount = 0; tcount < 1; tcount += speed) // t=0 is the start of the curve and t=1 is the end. object moves along curve according to the speed
        {

            enemyPos = Mathf.Pow(1 - tcount, 3) * p0 + 3 * Mathf.Pow(1 - tcount, 2) * tcount * p1 +
                3 * Mathf.Pow(1 - tcount, 1) * Mathf.Pow(tcount, 2) * p2 + Mathf.Pow(tcount, 3) * p3;



            transform.position = enemyPos;
            yield return new WaitForEndOfFrame();

            tcount = 0f;
            routeToGo += 1; // after the route is finished it goes to the next

            if (routeToGo > routes.Length - 1) //if there is no next route it goes back to the first
            {
                routeToGo = 0;
            }



            coroutineAllowed = true; //coroutine repeats

        }

    }
}
