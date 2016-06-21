using UnityEngine;
using System.Collections;

public class persistentStats : MonoBehaviour {

	public Color playerColor;
	public Equipment[] playerItems;
	public Equipment[] boughtItems;
	public int playerMoney;
    public string model;

    public Equipment[] player2Items;
    public Color player2Color;

    public bool tutorialActive = true;


	// Update is called once per frame
	void Update () {

	}

    void Awake()
    {
        DontDestroyOnLoad(this);

        playerItems = new Equipment[5];
        player2Items = new Equipment[5];
        for (int i = 0; i < 5; i++)
        {
            playerItems[i] = Equipment.EMPTY;
            player2Items[i] = Equipment.EMPTY;
        }

        boughtItems = new Equipment[10];



        for (int i = 0; i < 10; i++)
        { 
            boughtItems[i] = Equipment.EMPTY;
        }
        playerMoney = 1000;

        
        boughtItems[0] = Equipment.Item_Handle;
        boughtItems[1] = Equipment.Item_BasicEngine;


        model = "BaseMower";
	}
}
