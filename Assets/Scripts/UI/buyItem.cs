using UnityEngine;
using System.Collections;

public class buyItem : MonoBehaviour {
	
	private Vector3 startPos;
	public bool bought = false;
	
	private StoreController storeController;
	
	
	// Use this for initialization
	void Start () {
		storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
		startPos = this.transform.position;
        if (!this.transform.tag.Equals("Item_PlasmaShield") && !this.transform.tag.Equals("Item_CircularSaw"))
        {
            this.transform.position = new Vector3(startPos.x, startPos.y - 15, startPos.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!bought) 
		{
			for (int i = 0; i < storeController.AvailableItems.Length; i++) 
			{
				if (storeController.AvailableItems [i].ToString ().Equals (this.transform.tag)) 
				{
					this.transform.position = startPos;
					bought = true;
				}
			}
		}
	}
}
