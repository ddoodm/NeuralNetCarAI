using UnityEngine;
using System.Collections;
using System.Linq;
using System;

[RequireComponent(typeof(NeuralNet))]
public class NNCarController : MonoBehaviour
{
    public WheelCollider[] wheels;
    public Feeler[] feelers;

    public Rigidbody thisBody;
    public NeuralNet neuroNet;

    public float
        targetTorque = 25.0f;

    private Vector3
        initPosition;
    private Quaternion
        initRot;

    public float points { get; private set; }
    public float
        pointRewardTimerDuration = 2.5f,
        minPointReward = 0.5f;

    private float
        pointRewardTimer;

    public NNCarController() { }

    public NeuralNet baseNeuralNet { get; set; }

	// Use this for initialization
	void Start ()
    {
        initPosition = this.transform.position;
        initRot = this.transform.rotation;

        thisBody = this.GetComponent<Rigidbody>();
        neuroNet = this.GetComponent<NeuralNet>();
        neuroNet.baseNeuralNet = baseNeuralNet;

        pointRewardTimer = pointRewardTimerDuration;

        if (wheels.Length != 4)
            throw new System.Exception("The car must have four wheels.");

        LinkNeuralNet();
	}

    void LinkNeuralNet()
    {
        // Add inputs
        foreach (Feeler f in feelers)
            neuroNet.AddInput(f);

        // Add wheel torque outputs
        foreach(WheelCollider w in wheels)
            neuroNet.AddOutput(new NeuralNetTorqueOutput(w));

        // Add wheel steering outputs (front wheels only)
        for (int i=2; i<wheels.Length; i++)
            neuroNet.AddOutput(new NeuralNetSteerOutput(wheels[i]));

        neuroNet.MakeLinks();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pointRewardTimer > Time.deltaTime)
            pointRewardTimer -= Time.deltaTime;
	}

    public void ResetPosition()
    {
        transform.position = initPosition;
        transform.rotation = initRot;

        neuroNet.MakeLinks();
    }

    internal void AwardVictoryPoints(float prize)
    {
        points += Mathf.Max(minPointReward, prize * pointRewardTimer);
        pointRewardTimer = pointRewardTimerDuration;
    }
}
