using UnityEngine;
using System.Collections;

public class itemSelector : MonoBehaviour
{

    private Color startColour;
    private Color highlightItemColour = new Color(50f / 255f, 75f / 255f, 255f / 255f);
    private Color highlightSocketColour = new Color(0f / 255f, 200f / 255f, 0f / 255f);
    private string currentSelection;
    public bool isSocket;
    private bool highlighted = false;
    private bool bought;
    private weaponStats weapon;



    private StoreController storeController;

    public string objectName;
    public string objectWeight;
    

    
    void Start()
    {
        

        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        startColour = GetComponentInChildren<Renderer>().material.color;

        if (isSocket || GetComponent<Transform>().tag == "Phone_Model")
        {
            bought = true;
        }
        else
        {
            Debug.Log(this.name);
            bought = GetComponent<buyItem>().bought;
            Transform weaponObject = Resources.Load<Transform>("Item_" + this.name);
            weapon = weaponObject.GetComponent<weaponStats>();
            if (weapon == null)
            {
                Debug.Log("stat is in a child");
                weapon = weaponObject.GetComponentInChildren<weaponStats>();
            }
            objectWeight = weapon.mass*50 + "kg";
        }
    }


    void Update()
    {
        currentSelection = storeController.selectedEquipment.ToString();
        
        if (highlighted)
        {
            return;
        }

        if (isSocket || GetComponent<Transform>().tag == "Phone_Model")
        {
            bought = true;
        }
        else
        {
            bought = GetComponent<buyItem>().bought;
        }

        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {
                ResetColour(child);
            }
        }
    }
    

    void OnMouseEnter()
    {
        if (storeController.current_state != StoreState.STATE_ITEM)
        {
            return;
        }

        if (!bought)
        {
            return;
        }

        highlighted = true;
        
        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {
                ChangeColour(child);
            }
        }
       
    }


    void OnMouseExit()
    {
        highlighted = false;

        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {
                ResetColour(child);
            }
        }
        
    }


    public void ChangeColour(Transform transform)
    {
        if (transform.GetComponent<Renderer>() == null)
        {
            return;
        }

        transform.GetComponent<Renderer>().material.color = highlightItemColour;

        transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0f / 255f, 50f / 255f, 63.75f / 255f));

        if (isSocket)
        {
            transform.GetComponent<Renderer>().material.color = highlightSocketColour;
            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", highlightSocketColour);
            transform.GetComponent<Transform>().localScale = new Vector3(0.375f, 0.375f, 0.375f);
        }
    }


    public void ResetColour(Transform transform)
    {
        if (transform.GetComponent<Renderer>() == null)
        {
            return;
        }

        transform.GetComponent<Renderer>().material.color = startColour;
        transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        if (isSocket)
        {
            transform.GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }




    void OnGUI()
    {
        if (highlighted && !isSocket && !string.IsNullOrEmpty(objectName))
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 10, Event.current.mousePosition.y, 125, 40), objectName + "\n" + "Weight: " + objectWeight);
        }
    }
}
