using UnityEngine;
using System.Collections;

public class restartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void restart()
    {
        Application.LoadLevel("ItemStore");
    }
}
