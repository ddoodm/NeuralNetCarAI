using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class phoneScreenUI : MonoBehaviour {

    public Button button;
    public Text buyText;
    public int price;
	public Text description;
    public Equipment thisItem;
    StoreController storeController;
    private persistentStats stats;

    Color activeColour = new Color(0 / 255f, 225 / 255f, 30 / 255f);
    Color inactiveColour = new Color(125 / 255f, 200 / 255f, 125 / 255f);


    void Start () {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        stats = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

        buyText.text = "$" + price;
        if (storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
           button.interactable = false;
           button.image.color = inactiveColour;
        }
        else if (storeController.money >= price)
        {
            button.interactable = true;
            button.image.color = activeColour;
        }
		string name = this.name.Replace ("Screen_", "");
		Transform weaponObject = Resources.Load<Transform>("Item_" + name);
		weaponStats weapon = weaponObject.GetComponent<weaponStats>();
		if (weapon == null)
		{
			Debug.Log("stat is in a child");
			weapon = weaponObject.GetComponentInChildren<weaponStats>();
		}

		float damage = 0;
		if (weapon.damageMultiplier < 1) {
			damage = 0;
		} else {
			damage = (weapon.damageMultiplier - 1) * 10;
		}



		description.text = "Weight: " + weapon.mass * 50 + "kg" + "\n" + "Damage: " + damage;




    }


    void Update ()
    {
        if(storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
            button.interactable = false;
            button.image.color = inactiveColour;
        }
        else if (storeController.money >= price)
        {
            button.interactable = true;
            button.image.color = activeColour;
        }
    }
	

    public void DisableBuy()
    {
        if (storeController.money >= price)
        {
            button.interactable = false;
            button.image.color = inactiveColour;
            storeController.money -= price;
            storeController.AvailableItems[(int)thisItem] = thisItem;
            stats.playerMoney -= price;

        }
    }










}
