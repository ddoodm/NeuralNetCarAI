using UnityEngine;
using System.Collections;

public class PlayAudioOnCollision : MonoBehaviour
{
    public AudioSource source;

    public float
        minForce = 50.0f,
        maxVolume = 2.0f,
        soundDivider = 500.0f;

    void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float hitForce = collision.impulse.magnitude;

        if (hitForce >= minForce)
            source.PlayOneShot(source.clip, Mathf.Min(maxVolume, hitForce / soundDivider));
    }
}
