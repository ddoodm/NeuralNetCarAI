using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text clock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            int temp = (int)Time.timeSinceLevelLoad;
            clock.text = " " + temp;
        }
	
	}
}
