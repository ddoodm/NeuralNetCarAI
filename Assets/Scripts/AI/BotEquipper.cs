using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
public class BotEquipper : MonoBehaviour
{

    private int botWepLimit,
        botSocketLimit;
    
	// Use this for initialization
	void Start ()
    {
        // Jesse's code to add attachments to the bot

        switch (Application.loadedLevel)
        {
            case 3:
                botWepLimit = 4;
                botSocketLimit = 3;
                break;
            case 4:
                botWepLimit = 7;
                botSocketLimit = 4;
                break;
            case 5:
            case 1:
                botWepLimit = 9;
                botSocketLimit = 5;
                break;
        }
        AddAttachments();

        
    }

    public void AddAttachments()
    {
        Equipment[] botItems = new Equipment[5];
        for (int i = 0; i < 5; i++)
            botItems[i] = Equipment.EMPTY;

        int rand;
        for (int i = 0; i < botSocketLimit; i++)
        {
            rand = Random.Range(0, botWepLimit);

            switch (rand)
            {
                case 0:
                    if ((SocketLocation)i != SocketLocation.BACK)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Handle;
                    break;

                case 1:
                    if ((SocketLocation)i != SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_BasicEngine;
                    break;

                case 2:
                        botItems[i] = Equipment.Item_Spike;
                    break;

                case 3:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Flipper;
                    break;

                case 4:
                    if ((SocketLocation)i == SocketLocation.FRONT || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Booster;
                    break;

                case 5:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_MetalShield;
                    break;

                

                case 6:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_CircularSaw;
                    break;

                case 7:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Hammer;
                    break;

                case 8:
                    if ((SocketLocation)i != SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_PlasmaShield;
                    break;
                    

                default:
                    break;
            }
        }

        //botItems[3] = Equipment.Item_Handle;
        //botItems[(int)SocketLocation.BACK] = Equipment.Item_Booster;

        //ayy lmao
        GetComponent<SocketEquipment>().SocketItems(botItems, "BaseMower");
    }
}
