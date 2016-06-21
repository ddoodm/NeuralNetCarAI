using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setSliders : MonoBehaviour {

    public persistentStats playerChoice;
    public Slider slider;




	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
        {
            playerChoice = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();
        }

        slider = transform.GetComponent<Slider>();
        sliderStart();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void sliderStart()
    {
        switch (this.name)
        {
            case "r":
                slider.value = playerChoice.playerColor.r*255;
                break;
            case "g":
                slider.value = playerChoice.playerColor.g*255;
                break;
            case "b":
                slider.value = playerChoice.playerColor.b*255;
                break;
        }
    }
}
