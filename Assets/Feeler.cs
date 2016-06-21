using UnityEngine;
using System.Collections;
using System;

public class Feeler : MonoBehaviour, NeuralNetInput
{
    public bool colliding { get; private set; }

    private ArrayList cols = new ArrayList();

    private Renderer thisRenderer;

    void Start()
    {
        thisRenderer = this.GetComponent<Renderer>();
    }

    void Update()
    {
        // Update colour
        thisRenderer.material.color = colliding ? Color.red : Color.white;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "WorldGeom")
            return;

        cols.Add(col);

        this.colliding = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag != "WorldGeom")
            return;

        cols.Remove(col);

        if (cols.Count == 0)
            this.colliding = false;
    }

    public float GetNeuralNetInputValue()
    {
        return colliding ? 1.0f : -1.0f;
    }
}
