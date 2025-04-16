using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://pixabay.com/service/license-summary/

public class SoundManager : MonoBehaviour
{
    public AudioClip placingSound;
    public AudioClip zoneAttackSound;
    public AudioClip missileSound;
    public AudioClip missileExplosionSound;
    public AudioClip lazerFiring;

    private AudioSource missileAudioSource;
    public void PlayPlacingSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(placingSound, position);

    }
    public void PlayZoneAttackSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(zoneAttackSound, position);

    }
   
    public void StopMissileSound()
    {
        if (missileAudioSource != null && missileAudioSource.isPlaying)
        {
            missileAudioSource.Stop();
        }
    }

    public void PlayMissleExplosionSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(missileExplosionSound, position);

    }

    public void PlayLazerFireSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(lazerFiring, position);

    }

}
