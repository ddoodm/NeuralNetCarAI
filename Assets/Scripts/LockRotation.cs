using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour
{
    public Vector3 lockMask = new Vector3(1.0f, 0.0f, 1.0f);

	void Update ()
    {
        transform.Rotate(
            -Vector3.Scale(transform.rotation.eulerAngles, lockMask));
	}
}
