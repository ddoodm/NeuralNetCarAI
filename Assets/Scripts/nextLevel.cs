using UnityEngine;
using System.Collections;

public class nextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.LoadLevel(Application.loadedLevel + 1);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
