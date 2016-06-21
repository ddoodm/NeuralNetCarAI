using UnityEngine;
using System.Collections;
using System;

public class flipperControls : MonoBehaviour, Weapon {

    public float
        flipForce = 800.0f,
        proximityRadius = 1.0f;

    private float
        initialSpring, initialSpringTarget, initialRotation;

    public Vector3 initialRot = new Vector3(0,0,0);

    public bool canFlip { get; protected set; }

    Rigidbody thisRigidbody, opRigidbody;

    public float animationTime;
    public AnimationCurve curve;

	// Use this for initialization
	void Start ()
    {
        thisRigidbody = GetComponent<Rigidbody>();

        if (this.transform.root.tag == "Player")
        {
            GameObject temp = GameObject.FindWithTag("Enemy");
            if (temp != null)
                opRigidbody = temp.GetComponent<Rigidbody>();
            else
                opRigidbody = GameObject.FindWithTag("Player2").GetComponent<Rigidbody>();
        }
        else
        {
            FSMBotController thisController = this.transform.root.GetComponent<FSMBotController>();
            opRigidbody = thisController.playerTransform.GetComponent<Rigidbody>();
        }

        canFlip = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DoAnimation();
        DoAudio();
    }

    /// <summary>
    /// Implementation of Weapon.Use()
    /// </summary>
    public void Use()
    {
        if(canFlip)
            Flip();
    }

    public void EndUse()
    {
        // Not needed for the flipper
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    private void Flip()
    {
        canFlip = false;
        animationTime = 0;

        // The target may not have been collided with
        if (!opRigidbody)
            return;

        // If the flipper is close enough to the bot:
        if ((this.transform.position - opRigidbody.transform.position).magnitude < proximityRadius)
        {
            Vector3 normal = (opRigidbody.transform.position - this.transform.position).normalized;

            // Apply a reliable force to the opponent rigidbody
            opRigidbody.AddForceAtPosition(
                0.5f * (normal + Vector3.up) * flipForce,
                this.transform.position,
                ForceMode.Impulse);
        }

        // Rob's code to flip the player
        else if (Vector3.Dot(this.transform.root.up, Vector3.up) < 0)
            transform.root.GetComponentInParent<Rigidbody>().AddForceAtPosition(0.5f * Vector3.up * flipForce, this.transform.position, ForceMode.Impulse);
    }

    private void DoAnimation()
    {
        animationTime += Time.deltaTime;
        float animCurveValue = curve.Evaluate(animationTime);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, transform.parent.parent.parent.transform.rotation.eulerAngles.y - initialRot.y, animCurveValue * -180));

        // The player may flip once the animation is reset
        if (animationTime >= 1.0f)
            canFlip = true;
    }

    private void DoAudio()
    {
        if (animationTime >= 0 && GetComponent<AudioSource>().isPlaying == false && animationTime <= 1)
            GetComponent<AudioSource>().Play();
    }
}
