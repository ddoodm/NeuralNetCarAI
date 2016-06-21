using UnityEngine;
using System.Collections;

public class VehicleAudioManager : MonoBehaviour
{
    public AudioSource vehicleSource, engineSource;

    Rigidbody thisBody;

    public AudioClip
        impactClip,
        clangClip,
        engineIdle;

    void Start()
    {
        thisBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        EngineNoise();
    }

    void EngineNoise()
    {
        engineSource.clip = engineIdle;

        engineSource.pitch =
            Mathf.Clamp(
                0.75f +
                0.2f * thisBody.velocity.magnitude,
            0.5f, 1.8f);

        if (!engineSource.isPlaying)
            engineSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        float hitForce = collision.impulse.magnitude;

        if (hitForce >= 50.0f)
            vehicleSource.PlayOneShot(impactClip, Mathf.Min(2.0f, hitForce / 500.0f));

        if (hitForce >= 400.0f)
            vehicleSource.PlayOneShot(clangClip, Mathf.Min(2.0f, hitForce / 300.0f));
    }
}
