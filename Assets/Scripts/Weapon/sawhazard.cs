using UnityEngine;
using System.Collections;

public class sawhazard : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, speed));
	
	}
}
