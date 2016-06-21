using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    /// <summary>
    /// Wheel colliders at vehicle edges
    /// TODO: Create axle class
    /// </summary>
    public WheelCollider[] wheels;
	public bool play = true;

    public int player = 1;

    /// <summary>
    /// Motor, braking and steering constraints
    /// </summary>
    public float
        maxTorque = 50.0f,
        maxBrakeTorque = 50.0f,
        speedMultiplier = 1.0f,
        maxSteeringAngle = 20.0f,
        strafeSpeed,
        steeringAngle = 20.0f;

    /// <summary>
    /// Controller inputs obtained at frame update
    /// </summary>
    protected float
        inputLinearForce,
        inputSteering;

    void Update()
    {
		if (play) 
		{
            switch (player)
            {
                case 1:
                    {
                        // Update input forces
                        if (Input.GetKey(KeyCode.W) || Input.GetButton("Fire1") || Input.GetKey(KeyCode.UpArrow))
                            inputLinearForce = 1;
                        else if (Input.GetKey(KeyCode.S) || Input.GetButton("Fire2") || Input.GetKey(KeyCode.DownArrow))
                            inputLinearForce = -1;
                        else
                            inputLinearForce = 0;

                        //boost();

                        //inputLinearForce = Input.GetAxis ("Vertical");
                        inputSteering = Input.GetAxis("Horizontal");
                        break;
                    }
                case 2:
                    {
                        if (Input.GetKey(KeyCode.W) || Input.GetButton("Fire1P2") || Input.GetKey(KeyCode.UpArrow))
                            inputLinearForce = 1;
                        else if (Input.GetKey(KeyCode.S) || Input.GetButton("Fire2P2") || Input.GetKey(KeyCode.DownArrow))
                            inputLinearForce = -1;
                        else
                            inputLinearForce = 0;

                        //boost();

                        //inputLinearForce = Input.GetAxis ("Vertical");
                        inputSteering = Input.GetAxis("HorizontalP2");
                        break;
                    }
            }
		}
    }

    void FixedUpdate()
	{
		if (play)
        {
			// Front-wheel steering
            float temp = 1;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                temp = 4;
            }

			float steerAngle = inputSteering * steeringAngle * temp;
            wheels [0].steerAngle = wheels [1].steerAngle = steerAngle;

			// All-wheel drive
			float torque = inputLinearForce * maxTorque * speedMultiplier;
			for (int i = 0; i < wheels.Length; i++)
				wheels [i].motorTorque = torque;
		}
    }
}
