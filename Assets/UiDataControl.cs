using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiDataControl : MonoBehaviour {
    public Text speciesText, genText;
    public SimulationController sim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        speciesText.text = "S: " + sim.SpeciesNumber;
        genText.text = "G: " + sim.GenerationNumber;
	}
}
