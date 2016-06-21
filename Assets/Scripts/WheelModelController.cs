using UnityEngine;
using System;
using System.Collections;

public class WheelModelController : MonoBehaviour
{
    public Transform wheelColliderParent;

    private WheelCollider[] colliders;
    private Transform[] models;
    private Vector3[] defaultPositions;

	// Use this for initialization
	void Start ()
    {
        if (wheelColliderParent.childCount != this.transform.childCount)
            throw new Exception("The number of wheel models must match the number of wheel colliders.");

        colliders = new WheelCollider[wheelColliderParent.childCount];
        for(int i=0; i<wheelColliderParent.childCount; i++)
        {
            WheelCollider childCollider = wheelColliderParent.GetChild(i).GetComponent<WheelCollider>();
            if (childCollider)
                colliders[i] = childCollider;
        }

        models = new Transform[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
            models[i] = this.transform.GetChild(i);
    }
	
	void FixedUpdate ()
    {
        for(int i = 0; i < models.Length; i++)
        {
            WheelHit whit;
            if (colliders[i].GetGroundHit(out whit))
            {
                Vector3 wheelPoint =
                    whit.point + Vector3.up * colliders[i].radius;
                models[i].transform.position = wheelPoint;
            }
            else
                if(defaultPositions != null && defaultPositions.Length >= i)
                    models[i].transform.localPosition = defaultPositions[i];
        }

        if(defaultPositions == null)
        {
            defaultPositions = new Vector3[this.transform.childCount];
            for (int i = 0; i < models.Length; i++)
                defaultPositions[i] = models[i].localPosition + transform.up * 0.1f;
        }
    }
}
