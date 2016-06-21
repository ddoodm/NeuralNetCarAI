using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(WheelCollider))]
public class NeuralNetTorqueOutput : NeuralNetOutput
{
    public float targetTorque = 25.0f;

    private WheelCollider wheel;

    public NeuralNetTorqueOutput(WheelCollider wheel)
    {
        this.wheel = wheel;
    }

    public void SetNeuralNetOutput(float value)
    {
        wheel.motorTorque = targetTorque * (value+1.0f) / 2.0f;
    }
}

[RequireComponent(typeof(WheelCollider))]
public class NeuralNetSteerOutput : NeuralNetOutput
{
    public float maxAngle = 140.0f;

    private WheelCollider wheel;

    public NeuralNetSteerOutput(WheelCollider wheel)
    {
        this.wheel = wheel;
    }

    public void SetNeuralNetOutput(float value)
    {
        wheel.steerAngle = maxAngle * value;
    }
}