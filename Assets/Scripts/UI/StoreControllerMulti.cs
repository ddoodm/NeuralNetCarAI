using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreControllerMulti : MonoBehaviour {

	public GameObject player;
	public GameObject storeUI;
	public GameObject uiPlayerModels;
	GameObject selectedModel;

	public Vector3 rgbColor = new Vector3(200f/255f, 50f/255f, 50f/255f);
	private Color sliderColour;
	public Color selectedItemColour = new Color(255f/255f, 200f/255f, 0f/255f);

	public StoreState current_state = StoreState.STATE_MODEL;

    persistentStats playerChoice;

	int noOfModels;
	int playerModelPos = 0;

	//bool hasSpike = false;
    bool colourChange = false;

	//
	// Selected items on the model
	//
	private static int MAX_SOCKETS = 5;
	private static int TOTAL_ITEMS = 4;
	private Equipment[] itemSocketArray = new Equipment[MAX_SOCKETS];
	public Equipment selectedEquipment = Equipment.EMPTY;
	private Socket selectedSocket = Socket.EMPTY;

	public Equipment[] AvailableItems = new Equipment[TOTAL_ITEMS];


    //Robs super amazing variables
    public ItemSelectorMulti[] possibleObjects;
    public int controllerSelected = 0;
    private string controllerA = "SocketFront";
    private string controllerX = "SocketLeft";
    private string controllerB = "SocketRight";
    private string axisH = "Horizontal";
    private string controllerStart = "Start";
    private int playerNumber;
    private GameObject selectedItem;


	// Use this for initialization
	void Start () {

        

        possibleObjects = transform.parent.GetComponentsInChildren<ItemSelectorMulti>();
        playerNumber = possibleObjects[0].player;

        if (playerNumber == 2)
        {
            controllerA += "P2";
            controllerB += "P2";
            controllerX += "P2";
            axisH += "P2";
            controllerStart += "P2";

        }

        Time.timeScale = 1;

        if (GameObject.FindGameObjectWithTag ("Persistent Stats").GetComponent<persistentStats> () != null) 
		{
			playerChoice = GameObject.FindGameObjectWithTag ("Persistent Stats").GetComponent<persistentStats> ();
		}


		// equipment initializers=
		for (int i = 0; i < MAX_SOCKETS; i++)
		{
			itemSocketArray[i] = Equipment.EMPTY;
		}


		if (playerChoice != null){
			for (int i = 0; i < MAX_SOCKETS; i++)
			{
				playerChoice.playerItems[i] = itemSocketArray[i];
			}

			//get the bought items
			for (int i = 0; i < TOTAL_ITEMS; i++)
			{
				AvailableItems[i] = playerChoice.boughtItems[i];
			}

		}

		noOfModels = uiPlayerModels.GetComponent<Transform> ().childCount - 1;

		sliderColour = new Color(rgbColor.x, rgbColor.y, rgbColor.z);
	}


	// Update is called once per frame
	void Update () {
        
        controllerHighlight();

        if (selectedEquipment != Equipment.EMPTY)
        {
            foreach (Transform child in selectedItem.transform)
            {
                child.GetComponent<Renderer>().material.color = selectedItemColour;
            }
            if (selectedSocket != Socket.EMPTY)
            {
                FillItemSocketArray();
            }

        }

        sliderColour = new Color(rgbColor.x / 255, rgbColor.y / 255, rgbColor.z / 255);
        Renderer[] parts = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer part in parts)
        {
            if(part.tag != "Socket_Ball")
                part.material.color = sliderColour;
        }
        
        if (playerChoice != null) 
		{
            switch (playerNumber)
            {
                case 1:
                    playerChoice.playerColor = sliderColour;
                    for (int i = 0; i < MAX_SOCKETS; i++)
                    {
                        playerChoice.playerItems[i] = itemSocketArray[i];
                    }
                    break;
                case 2:
                    playerChoice.player2Color = sliderColour;
                    for (int i = 0; i < MAX_SOCKETS; i++)
                    {
                        playerChoice.player2Items[i] = itemSocketArray[i];
                    }
                    break;
            }
		}

        if(Input.GetButtonDown(controllerStart))
        {
            startTest();
        }

	}

    void controllerSliders()
    {
        if (possibleObjects[controllerSelected].isUI)
        {
            possibleObjects[controllerSelected].transform.parent.GetComponentInParent<Slider>().value += Input.GetAxis(axisH);
        }
    }

    void controllerHighlight()
    {
        if (current_state == StoreState.STATE_ITEM)
        {
            foreach (ItemSelectorMulti item in possibleObjects)
            {
                item.highlighted = false;
            }
            if (controllerSelected >= possibleObjects.Length)
            {
                controllerSelected = 0;
            }
            if (controllerSelected < 0)
            {
                controllerSelected = possibleObjects.Length - 1;
            }
            possibleObjects[controllerSelected].highlighted = true;
            controllerSliders();
            if (Input.GetButtonDown(controllerA) && possibleObjects[controllerSelected].isUI == false)
            {
                HandleItemSelection(possibleObjects[controllerSelected]);
            }
            if(Input.GetButtonDown(controllerX))
            {
                controllerSelected -= 1;
            }
            if (Input.GetButtonDown(controllerB))
            {
                controllerSelected += 1;
            }
        }
        else
        {
            if (Input.GetButtonDown(controllerA))
            {
                player.GetComponent<Transform>().Translate(new Vector3(0, 5.75f, 0), Space.World);


                current_state = StoreState.STATE_ITEM;
                GetComponent<Animator>().SetTrigger("toItemSelection");
            }
        }
    }

    void HandleItemSelection(ItemSelectorMulti item)
    {
        selectedSocket = Socket.EMPTY;
        switch (item.tag)
        {
            case "Item_Handle":
                selectedItem = item.gameObject;
                selectedEquipment = Equipment.Item_Handle;
                break;

            case "Item_Spike":
                selectedItem = item.gameObject;
                selectedEquipment = Equipment.Item_Spike;
                break;

            case "Item_Flipper":
                selectedItem = item.gameObject;
                selectedEquipment = Equipment.Item_Flipper;
                break;

            case "Item_Booster":
                selectedItem = item.gameObject;
                selectedEquipment = Equipment.Item_Booster;
                break;

            case "Socket_Left":
                selectedSocket = Socket.Socket_Left;
                break;

            case "Socket_Right":
                selectedSocket = Socket.Socket_Right;
                break;

            case "Socket_Front":
                selectedSocket = Socket.Socket_Front;
                break;

            case "Socket_Back":
                selectedSocket = Socket.Socket_Back;
                break;

            case "Socket_Top":
                selectedSocket = Socket.Socket_Top;
                break;

            case "Player Model":
                break;

            default:
                selectedEquipment = Equipment.EMPTY;
                selectedSocket = Socket.EMPTY;
                break;
        }

        if (selectedEquipment == Equipment.Item_Handle && (selectedSocket != Socket.Socket_Back && selectedSocket != Socket.EMPTY))
        {
            selectedSocket = Socket.EMPTY;
        }
        if (selectedEquipment == Equipment.Item_Spike && selectedSocket == Socket.Socket_Top)
        {
            selectedSocket = Socket.EMPTY;
        }
        if (selectedEquipment == Equipment.Item_Flipper && (selectedSocket == Socket.Socket_Top || selectedSocket == Socket.Socket_Back))
        {
            selectedSocket = Socket.EMPTY;
        }

        if (selectedEquipment == Equipment.Item_Booster && (selectedSocket == Socket.Socket_Top || selectedSocket == Socket.Socket_Front))
        {
            selectedSocket = Socket.EMPTY;
        }

        

    }
    


	// instantiate at sockets change rotation based on what socket. make this a separate script on the vehicle prefab
	
	void FillItemSocketArray(){
		switch (selectedSocket) {
			case Socket.Socket_Left:
				itemSocketArray[0] = selectedEquipment;
				break;

			case Socket.Socket_Right:
				itemSocketArray[1] = selectedEquipment;
				break;

			case Socket.Socket_Front:
				itemSocketArray[2] = selectedEquipment;
				break;

			case Socket.Socket_Back:
				itemSocketArray[3] = selectedEquipment;
				break;

			case Socket.Socket_Top:
				itemSocketArray[4] = selectedEquipment;
				break;

			default:
				selectedEquipment = Equipment.EMPTY;
				selectedSocket = Socket.EMPTY;
				return;

		}
		selectedEquipment = Equipment.EMPTY;
		selectedSocket = Socket.EMPTY;

		player.GetComponent<SocketEquipment>().SocketItems(itemSocketArray, "BaseMower");

	}


	public void startTest(){       
        Application.LoadLevel("BattleScene03Multi");
	}







	public void MoveLeft() {
		if (playerModelPos > 0)
		{
			playerModelPos--;
			StartCoroutine ("SlideLeft");
		}
	}

	public void MoveRight() {
		if (playerModelPos < noOfModels)
		{
			playerModelPos++;
			StartCoroutine ("SlideRight");
		}
	}


	private IEnumerator SlideLeft(){
		Vector3 oldPos;
		for (int i = 0; i < 10; i++) 
		{
			oldPos = uiPlayerModels.GetComponent<Transform> ().position;
			uiPlayerModels.GetComponent<Transform>().position = new Vector3(oldPos.x + 1, oldPos.y, oldPos.z);
			yield return null;
		}
	}
	
	private IEnumerator SlideRight(){
		Vector3 oldPos;
		for (int i = 0; i < 10; i++) 
		{
			oldPos = uiPlayerModels.GetComponent<Transform> ().position;
			uiPlayerModels.GetComponent<Transform>().position = new Vector3(oldPos.x - 1, oldPos.y, oldPos.z);
			yield return null;
		}
	}

    

    public void changeB(float slider)
    {
		rgbColor.z = slider;
    }

    public void changeR(float slider)
    {
        rgbColor.x = slider;
    }
    public void changeG(float slider)
    {
        rgbColor.y = slider;
    }

	public void BackToItems()
	{
		current_state = StoreState.STATE_ITEM;
		GetComponent<Animator>().SetTrigger("toItemSelection");
	}

}

