using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class replay : MonoBehaviour {

    public GameObject gameUI;

    public List<Transform> frames2 = new List<Transform>();

    public List<Transform> currentFrame = new List<Transform>();

    
    

    public float counter = 0;




	// Use this for initialization
	void Start () {
        gameUI = GameObject.FindGameObjectWithTag("UI");
	
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        for (int i = 0; i < currentFrame.Count; i++)
        {
            currentFrame[i] = frames2[i + (int)counter * currentFrame.Count];
        }

        if (counter >= 5)
        {
            counter = 0;
            frames2 = new List<Transform>();
        }
        
        
	
	}
}
