using UnityEngine;
using System.Collections;
using System;

public class BoosterControls : MonoBehaviour, Weapon
{
    SocketLocation location;

    public float
        strafeSpeed,
        forwardBoostForce = 5000.0f;

    public bool
        thrusting,
        forwardBoosting;

    // From Weapon interface
    public void Use()
    {
        Boost();
    }

    // From Weapon interface
    public void EndUse()
    {
        EndBoost();
    }

    // From Weapon interface
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Use this for initialization
    void Start ()
    {
        switch(transform.parent.name)
        {
            case "LeftSocket": location = SocketLocation.LEFT; break;
            case "RightSocket": location = SocketLocation.RIGHT; break;
            case "BackSocket": location = SocketLocation.BACK; break;
        }
	}

    private void Boost()
    {
        switch(location)
        {
            case SocketLocation.LEFT:
                thrusting = true;
                StartCoroutine("strafeParticles");
                transform.root.GetComponent<Rigidbody>().AddForce(transform.root.right * strafeSpeed);
                transform.root.GetComponent<EnergyController>().DrainEnergy();
                break;
            case SocketLocation.RIGHT:
                thrusting = true;
                StartCoroutine("strafeParticles");
                transform.root.GetComponent<Rigidbody>().AddForce(-transform.root.right * strafeSpeed);
                transform.root.GetComponent<EnergyController>().DrainEnergy();
                break;
            case SocketLocation.BACK:
                thrusting = true;
                forwardBoosting = true;
                //transform.root.GetComponent<VehicleController>().boosting = true;
                break;
        }
    }

    private void EndBoost()
    {
        if(location == SocketLocation.BACK)
        {
            thrusting = false;
            forwardBoosting = false;
            //transform.root.GetComponent<VehicleController>().boosting = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        EnergyController energyCtrl = transform.root.GetComponent<EnergyController>();
        //VehicleController vehicle = transform.root.GetComponent<VehicleController>();
        Rigidbody rootBody = transform.root.GetComponent<Rigidbody>();
        float maxEnergy = energyCtrl.maxEnergy;

        if (energyCtrl.energy > 0 && forwardBoosting)
        {
            energyCtrl.energy--;
            rootBody.AddForce(rootBody.transform.forward * forwardBoostForce * energyCtrl.unitEnergy);
            //vehicle.speedMultiplier = 5;
        }
        if (energyCtrl.energy <= 0)
        {
            //vehicle.speedMultiplier = 1;
            forwardBoosting = false;
        }
        /*
        if (!forwardBoosting && energyCtrl.energy < maxEnergy)
        {
            energyCtrl.energy += 0.25f;
            //vehicle.speedMultiplier = 1;
        }
        */

        thrustParticles();
	}

    private void thrustParticles()
    {
        if (thrusting)
        {
            if (this.GetComponent<AudioSource>().isPlaying == false)
            {
                this.GetComponent<AudioSource>().volume = 0.3f;
                this.GetComponent<AudioSource>().Play();
                
            }
            this.GetComponentInChildren<ParticleEmitter>().minSize = 0.9f;
            this.GetComponentInChildren<ParticleEmitter>().maxSize = 0.95f;
            this.GetComponentInChildren<ParticleAnimator>().doesAnimateColor = true;
        }
        else
        {
            this.GetComponent<AudioSource>().volume -= 0.01f;
            this.GetComponentInChildren<ParticleEmitter>().minSize = 0.5f;
            this.GetComponentInChildren<ParticleEmitter>().maxSize = 0.55f;
            this.GetComponentInChildren<ParticleAnimator>().doesAnimateColor = false;
        }

    }

    IEnumerator strafeParticles()
    {
        yield return new WaitForSeconds(1.0f);
        thrusting = false;
        StopCoroutine("strafeParticles");
    }
}
