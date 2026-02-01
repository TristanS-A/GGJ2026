using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    private void OnCollisionEnter(Collision collision)
    {
        particle.gameObject.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        particle.gameObject.transform.position = collision.contacts[0].point;

        particle.Play();
    }
}
