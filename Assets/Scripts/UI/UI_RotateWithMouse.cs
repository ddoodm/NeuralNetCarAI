using UnityEngine;
using System.Collections;

public class UI_RotateWithMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f);
        mousePos *= 45.0f;

        mousePos = new Vector3(-mousePos.y, mousePos.x, 0.0f);

        Vector3 rot = mousePos - this.transform.rotation.eulerAngles;
        this.transform.Rotate(rot);
	}
}
