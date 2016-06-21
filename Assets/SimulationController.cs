using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulationController : MonoBehaviour
{
    public NNCarController car_prefab;
    public VictoryFences fences;

    private NNCarController car, bestCar;
    private Generation generation;
    private List<Generation> generations;

    public int
        species = 10;

    public float
        minCarVelocity = 0.1f,
        carVelocityTimeout = 0.5f;

    private float
        carVelocityTimer;

    public int SpeciesNumber { get { return generation.Count; } }
    public int GenerationNumber { get { return generations.Count; } }

    private void MakeANewCar()
    {
        NNCarController newCar = GameObject.Instantiate(car_prefab);

        if(bestCar)
            newCar.baseNeuralNet = bestCar.neuroNet;

        generation.Add(newCar);
        car = newCar;
    }

	// Use this for initialization
	void Start ()
    {
        generation = new Generation();
        generations = new List<Generation>();

        generations.Add(generation);

        // Set up our first car in this generation
        MakeANewCar();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (car.thisBody.velocity.magnitude < minCarVelocity)
            carVelocityTimer += Time.deltaTime;
        else
            carVelocityTimer = 0;

        if (carVelocityTimer >= carVelocityTimeout)
            NextMutation();
	}

    void NextMutation()
    {
        fences.Reset();

        // Game over for this one; kill (hide) it
        car.transform.root.gameObject.SetActive(false);

        // Check whether we want a new car, or a new generation
        if (generation.Count >= species)
        {
            // Get the best car of this generation
            bestCar = generation.GetBestCar();

            // Make a new generation
            Generation newGen = new Generation();
            generations.Add(newGen);
            generation = newGen;
        }

        MakeANewCar();
    }
}
