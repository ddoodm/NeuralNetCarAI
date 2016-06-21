using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameObject car;
    private Vector3 offset;

    void FindCar()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("NNCar");
        foreach (GameObject c in cars)
            if (c.activeInHierarchy)
            {
                car = c;
                return;
            }
        car = null;
    }

	// Use this for initialization
	void Start ()
    {
        FindCar();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (car == null || !car.activeInHierarchy)
            FindCar();

        transform.position = car.transform.position + Vector3.up * 10.0f;
	}
}
