using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Neuron : NeuralNetInput, NeuralNetOutput
{
    private float
        value,
        weight;

    public List<NeuralNetInput> inputs;
    public List<NeuralNetOutput> outputs;

    public Neuron()
    {
        inputs = new List<NeuralNetInput>();
        outputs = new List<NeuralNetOutput>();

        RandomizeWeight();
    }

    public void RandomizeWeight()
    {
        weight = Random.Range(-1.0f, 1.0f);
    }

    public void Update()
    {
        value = 0;

        foreach (NeuralNetInput i in inputs)
            value += i.GetNeuralNetInputValue() / inputs.Count;

        foreach(NeuralNetOutput o in outputs)
            o.SetNeuralNetOutput(this.GetNeuralNetInputValue());
    }

    public float GetNeuralNetInputValue()
    {
        // This is where the input is modified and output!
        return weight * value;
    }

    public void SetNeuralNetOutput(float value)
    {
        this.value = value;
    }
}