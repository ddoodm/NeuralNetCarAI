using UnityEngine;
using System.Collections;

public class DisableAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioListener.volume = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
