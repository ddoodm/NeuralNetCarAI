using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSelectorMulti : MonoBehaviour
{

    private Color startColour;
    private Color highlightItemColour = new Color(50f / 255f, 75f / 255f, 255f / 255f);
    private Color highlightSocketColour = new Color(0f / 255f, 200f / 255f, 0f / 255f);
    private string currentSelection;
    public bool isSocket;
    public bool isUI;
    public bool highlighted = false;
    public int player;

    private StoreControllerMulti storeController;

    void Start()
    {
        storeController = transform.root.GetComponentInChildren<StoreControllerMulti>();
        //storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreControllerMulti>();
        if (isUI)
        {
            startColour = GetComponent<Image>().color;
        }
        else
            startColour = GetComponentInChildren<Renderer>().material.color;
    }

    void Update()
    {


        if (!highlighted)
        {
            if (GetComponent<Transform>().tag != storeController.selectedEquipment.ToString())
            {
                if (isUI)
                {
                    GetComponent<Image>().color = startColour;
                }
                else if (!isSocket)
                {
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Renderer>().material.color = startColour;
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        if (child.GetComponent<Renderer>() != null)
                        {
                            child.GetComponent<Renderer>().material.color = startColour;
                            child.GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        }
                    }
                }
            }
            else
            {
                foreach (Renderer child in transform.GetComponentsInChildren<Renderer>())
                {
                    child.material.color = storeController.selectedItemColour;
                }
            }

        }
        else
        {
            if (GetComponent<Transform>().tag != storeController.selectedEquipment.ToString())
            {
                if (isUI)
                {
                    GetComponent<Image>().color = highlightItemColour;
                }
                else if (!isSocket)
                {
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Renderer>().material.color = highlightItemColour;
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        if (child.GetComponent<Renderer>() != null)
                        {
                            child.GetComponent<Renderer>().material.color = highlightSocketColour;
                            child.GetComponent<Transform>().localScale = new Vector3(0.375f, 0.375f, 0.375f);
                        }
                    }
                }
            }
                /*
            else
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Renderer>().material.color = storeController.selectedItemColour;
                }
            }*/
        }

    }



    void OnMouseEnter()
    {
        if (storeController.current_state != StoreState.STATE_ITEM)
            return;
        highlighted = true;
    }


    void OnMouseExit()
    {
        highlighted = false;
    }
}
