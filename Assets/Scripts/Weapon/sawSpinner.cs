using UnityEngine;
using System.Collections;

public class sawSpinner : MonoBehaviour{

    public float spinSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DoAnimation();
	
	}

    private void DoAnimation()
    {
        this.transform.Rotate(new Vector3(0, 0,spinSpeed));
    }


}
