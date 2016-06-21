using UnityEngine;
using System.Collections;

public class Billboarder : MonoBehaviour
{
    public Camera camera;

    public GameObject top;
    public GameObject main;
    public GameObject mini;

    public bool state;

    void Start ()
    {
        if (camera == null)
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        main = camera.gameObject;
        top = GameObject.FindWithTag("Topdown Camera");
        mini = GameObject.FindWithTag("Minimap Camera");

        state = false;
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) && state == false)
        {
            state = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && state)
        {
            state = false;
        }

        cameras();
    }

    void cameras()
    {
        if (state)
        {
            
            main.SetActive(false);
            mini.SetActive(false);
            top.SetActive(true);
            this.transform.rotation =
            Quaternion.LookRotation(this.transform.position - top.transform.position, Vector3.up);
        }
        else
        {
            
            main.SetActive(true);
            mini.SetActive(true);
            top.SetActive(false);
            this.transform.rotation =
            Quaternion.LookRotation(this.transform.position - camera.transform.position, Vector3.up);
        }
    }
}
