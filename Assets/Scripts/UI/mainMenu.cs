using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

    public GameObject menu,
        credits;
    public int state = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case 0:
                menu.SetActive(true);
                credits.SetActive(false);
                break;
            case 1:
                menu.SetActive(false);
                credits.SetActive(true);
                break;

        }
	
	}


    public void singlePlayer()
    {
        Application.LoadLevel("ItemStore");
    }

    public void multiPlayer()
    {
        Application.LoadLevel("MultiStore");
    }

    public void creditsActivate()
    {
        state = 1;
    }

    public void creditsDeactivate()
    {
        state = 0;
    }

    public void quit()
    {
        Application.Quit();
    }
}
