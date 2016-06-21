using UnityEngine;
using System.Collections;

// does anyone else love magic numbers :P
public class PhoneNavigation : MonoBehaviour {

    private enum Screen { LEMPTY = -1, Screen_Handle = 0, Screen_BasicEngine, Screen_Spike, Screen_Flipper, Screen_Booster, Screen_MetalShield, Screen_CircularSaw, Screen_Hammer, Screen_PlasmaShield, Screen_TeslaCoil, REMPTY };
    
    Screen leftScreen;
    Screen centerScreen;
    Screen rightScreen;

    bool changingScreen;

    StoreController storeController;
    RectTransform[] screenList = new RectTransform[10];

    void Start()
    {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        leftScreen = Screen.LEMPTY;
        centerScreen = leftScreen + 1;
        rightScreen = centerScreen + 1;

        changingScreen = false;

        Screen temp = Screen.LEMPTY + 1;
        for (int i = 0; i < StoreController.TOTAL_ITEMS; i++)
        {
            screenList[i] = GameObject.Find((temp + i).ToString()).GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (leftScreen == Screen.LEMPTY)
        {
            leftScreen = Screen.REMPTY - 1;
        }

        if (rightScreen == Screen.REMPTY)
        {
            rightScreen = Screen.LEMPTY + 1;
        }
    }


    public void PreviousItem()
    {
        if (!changingScreen)
        {
            changingScreen = true;
            StartCoroutine("ItemsRight");
        }
    }


    public void NextItem()
    {
        if (!changingScreen)
        {
            changingScreen = true;
            StartCoroutine("ItemsLeft");
        }
    }


    private IEnumerator ItemsRight()
    {
        Vector3 currentPos, leftPos;
        RectTransform current = screenList[(int)centerScreen];
        RectTransform left = screenList[(int)leftScreen];

        if (left.anchoredPosition3D.z != 0)
        {
            left.anchoredPosition3D = new Vector3(-700.0f, 130.0f, 0.0f);
        }

        for (int i = 0; i < 10; i++)
        {
            currentPos = current.anchoredPosition3D;
            leftPos = left.anchoredPosition3D;

            current.anchoredPosition3D = new Vector3(currentPos.x + 70, currentPos.y, currentPos.z);
            left.anchoredPosition3D = new Vector3(leftPos.x + 70, leftPos.y, leftPos.z);
            
            yield return null;
        }
        current.anchoredPosition3D = new Vector3(0.0f, 700.0f, 10.0f);

        rightScreen = centerScreen;
        centerScreen = leftScreen;
        leftScreen -= 1;

        changingScreen = false;

        if (leftScreen == Screen.LEMPTY)
        {
            leftScreen = Screen.REMPTY - 1;
        }
    }


    private IEnumerator ItemsLeft()
    {
        Vector3 currentPos, rightPos;
        RectTransform current = screenList[(int)centerScreen];
        RectTransform right = screenList[(int)rightScreen];

        if (right.anchoredPosition3D.z != 0)
        {
            right.anchoredPosition3D = new Vector3(700.0f, 130.0f, 0.0f);
        }

        for (int i = 0; i < 10; i++)
        {
            currentPos = current.anchoredPosition3D;
            rightPos = right.anchoredPosition3D;

            current.anchoredPosition3D = new Vector3(currentPos.x - 70, currentPos.y, currentPos.z);
            right.anchoredPosition3D = new Vector3(rightPos.x - 70, rightPos.y, rightPos.z);

            yield return null;
        }
        current.anchoredPosition3D = new Vector3(0.0f, 700.0f, 10.0f);

        leftScreen = centerScreen;
        centerScreen = rightScreen;
        rightScreen += 1;

        changingScreen = false;
        
        if (rightScreen == Screen.REMPTY)
        {
            rightScreen = Screen.LEMPTY + 1;
        }
    }
}
