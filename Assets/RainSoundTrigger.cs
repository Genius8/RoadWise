using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundTrigger : MonoBehaviour
{
    public AudioSource rainAudioSource;
    private float _timeSinceLastHit = 0f;
    public float stopDelay = 1.5f; // Time in seconds before sound stops after last hit

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Rain particle hit: " + other.name);

        _timeSinceLastHit = 0f;

        if (!rainAudioSource.isPlaying)
        {
            rainAudioSource.Play();
        }
    }

    private void Update()
    {
        if (rainAudioSource.isPlaying)
        {
            _timeSinceLastHit += Time.deltaTime;

            if (_timeSinceLastHit > stopDelay)
            {
                rainAudioSource.Stop();
            }
        }
    }
}
