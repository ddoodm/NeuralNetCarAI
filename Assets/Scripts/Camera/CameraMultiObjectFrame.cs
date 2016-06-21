using UnityEngine;
using System;
using System.Collections;

public class CameraMultiObjectFrame : MonoBehaviour
{
    public Transform[] objects;

    float butter = 1.8f;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (objects.Length < 2)
            throw new Exception("The CameraMultiObjectFrame controller needs at least two objects to follow.");

        Vector3 posSum = Vector3.zero;

        foreach(Transform t in objects)
            posSum += t.position;
        Vector3 midpoint = posSum / objects.Length;

        Vector3 targetPos = new Vector3(
            this.transform.position.x,
            (objects[0].position - objects[1].position).magnitude,
            this.transform.position.z
            );

        transform.position = targetPos;

        //Vector3 dampPos = (targetPos - transform.position) * butter * Time.deltaTime;
        //transform.Translate(dampPos);

        this.transform.rotation = Quaternion.LookRotation(
            (midpoint - this.transform.position).normalized
            );
    }
}
