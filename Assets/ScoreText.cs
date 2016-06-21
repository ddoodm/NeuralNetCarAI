using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour
{
    private NNCarController car;
    private TextMesh text;

	// Use this for initialization
	void Start () {
        car = transform.root.GetComponent<NNCarController>();
        text = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = car.points.ToString();
	}
}
