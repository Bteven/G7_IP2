using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 10f;
    public float homingStrength = 5f;
    private Transform targetEnemy;

    private AudioSource audioSource;
   



    private void Awake()
    {
              

        audioSource = GetComponent<AudioSource>();
                 
        audioSource.volume = 0.7f;
              
        audioSource.spatialBlend = 1f;  
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 20f;

        // this just makes the missile sound nicer by adjusting the audio source
    }
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
