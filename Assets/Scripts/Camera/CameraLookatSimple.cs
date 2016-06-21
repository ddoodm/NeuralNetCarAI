using UnityEngine;
using System.Collections;

public class CameraLookatSimple : MonoBehaviour
{
    public Transform target;
    public float
        targetFOV = 40.0f,
        maxDist = 10.0f;


    //This was giving me errors
    private new Camera camera;

	// Use this for initialization
	void Start ()
    {
        camera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.rotation = Quaternion.LookRotation(target.position - this.transform.position);

        float distanceUnit = 1.0f - (target.position - this.transform.position).magnitude / maxDist;
        camera.fieldOfView = distanceUnit * targetFOV;
	}
}
