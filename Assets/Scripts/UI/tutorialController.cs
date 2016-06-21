using UnityEngine;
using System.Collections;

public class tutorialController : MonoBehaviour {

    private persistentStats stats;
    public bool active;
    public GameObject[] tutorialMessages;


	// Use this for initialization
	void Start () {
        stats = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();
        active = stats.tutorialActive;

        tutorialMessages = GameObject.FindGameObjectsWithTag("Tutorial");
        foreach (GameObject message in tutorialMessages)
        {
            message.gameObject.SetActive(active);
        }	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            active = !active;
            stats.tutorialActive = active;
        }
        foreach (GameObject message in tutorialMessages)
        {
            if(message != null)
                message.gameObject.SetActive(active);
        }
	
	}
}
