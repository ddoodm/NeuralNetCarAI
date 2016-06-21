using UnityEngine;
using System.Collections;

public class PlasmaEffect : MonoBehaviour {

    public float speed;
    float lightSpeed = 100f;
    public bool hasLight;


    void FixedUpdate() {
        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), speed * Time.deltaTime);
    }


    void Update()
    {
        if (hasLight)
        {
            float
                redSinus = Mathf.Sin(Time.time * 4.0f),
                greenSinus = Mathf.Sin(Time.time * 5.0f),
                blueSinus = Mathf.Cos(Time.time * 4.5f);

            Color newColour = new Color(
                0.3f + 0.2f * redSinus,
                0.4f + 0.2f * greenSinus,
                0.5f + 0.2f * blueSinus
                );

            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColour);
            transform.GetComponent<Renderer>().material.color = newColour;

            Light[] lights = this.GetComponentsInChildren<Light>();

            foreach (Light child in lights)
            {
                child.color = newColour;
            }
        }
    }


}
