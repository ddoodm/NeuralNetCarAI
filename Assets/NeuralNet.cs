using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface NeuralNetInput
{
    float GetNeuralNetInputValue();
}

public interface NeuralNetOutput
{
    void SetNeuralNetOutput(float value);
}

public class NeuralNet : MonoBehaviour
{
    private List<NeuralNetInput> inputs = new List<NeuralNetInput>();
    private List<NeuralNetOutput> outputs = new List<NeuralNetOutput>();

    private List<List<Neuron>> nodes;

    public NeuralNet baseNeuralNet { get; set; }

    // Use this for initialization
    void Awake ()
    {
        nodes = new List<List<Neuron>>();

        // Create input and output node lists
        for(int i=0; i<2; i++) nodes.Add(new List<Neuron>());
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Update all nodes
        foreach(List<Neuron> l in nodes)
            foreach(Neuron n in l)
                n.Update();
	}

    internal void MakeLinks()
    {
        // TEST LINKAGE
        //nodes[0][0].outputs.Add(nodes[1][0]);
        //nodes[1][0].inputs.Add(nodes[0][0]);

        // Make random middle nodes
        int numInternalLayers = Random.Range(1, 2);
        for (int i = 0; i < numInternalLayers; i++)
        {
            nodes.Add(new List<Neuron>());
            int numMidNodes = Random.Range(1, 10);
            for (int j = 0; j < numMidNodes; j++)
                nodes[i+2].Add(new Neuron());
        }

        // Random links from inputs to middle
        for(int i=0; i<nodes[0].Count; i++)
        {
            // Shuffle up weights
            nodes[0][i].RandomizeWeight();

            // Find buddy (right)
            int buddy = Random.Range(0, nodes[2].Count - 1);
            nodes[0][i].outputs.Add(nodes[2][buddy]);
            nodes[2][buddy].inputs.Add(nodes[0][i]);
        }

        // Random links from middle to outputs
        for (int i = 0; i < nodes[1].Count; i++)
        {
            // Shuffle up weights
            nodes[0][i].RandomizeWeight();

            // Find buddy (left)
            int lastIdx = nodes.Count - 1;
            int buddy = Random.Range(0, nodes[lastIdx].Count - 1);
            nodes[1][i].inputs.Add(nodes[lastIdx][buddy]);
            nodes[lastIdx][buddy].outputs.Add(nodes[1][i]);
        }

        // Random internal links
        if (nodes.Count > 3)
        {
            for (int i = 2; i < nodes.Count-1; i++)
            {
                int numLinks = Random.Range(1, 3);
                for (int j = 0; j < numLinks; j++)
                {
                    int buddy = Random.Range(0, nodes[i + 1].Count);
                    nodes[i][j].outputs.Add(nodes[i+1][buddy]);
                    nodes[i+1][buddy].inputs.Add(nodes[i][j]);
                }
            }
        }

        int apple = 2;
    }

    public void AddInput(NeuralNetInput input)
    {
        inputs.Add(input);

        Neuron node = new Neuron();
        node.inputs.Add(input);
        nodes[0].Add(node);
    }

    public void AddOutput(NeuralNetOutput output)
    {
        outputs.Add(output);

        Neuron node = new Neuron();
        node.outputs.Add(output);
        nodes[1].Add(node);
    }
}
