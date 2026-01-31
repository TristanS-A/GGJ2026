using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public float velocityThreshold = 0.5f;

    [Header("Audio Settings")]
    public float cooldown = 0.2f;
    private float lastPlayTime;

    // Using to make it not constantly keep going.
    [Header("Thresholds")]
    public float minVelocity = 0.8f;
    public float maxVelocity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPlayTime = -cooldown;
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        // Use the impact force to determine the sound because it kept being too much
        if(impactForce > minVelocity && Time.time >= lastPlayTime + cooldown)
        {
            float volume = Mathf.InverseLerp(0, maxVelocity, impactForce);
            audioSource.pitch = Random.Range(0.85f, 1.15f);
            audioSource.PlayOneShot(audioSource.clip, volume);
            lastPlayTime = Time.time;
        }
    }
}
