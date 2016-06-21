using UnityEngine;
using System.Collections;

public class playVid : MonoBehaviour {

	// Use this for initialization
	void Start () {

#if !UNITY_WEBGL
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
#endif

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
