using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 10f;
    public float homingStrength = 5f;
    private Transform targetEnemy;

    void Update()
    {
        if (targetEnemy != null)
        {
            Vector3 direction = targetEnemy.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * homingStrength);
            transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }
}
