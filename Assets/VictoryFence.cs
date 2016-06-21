using UnityEngine;
using System.Collections;

public class VictoryFence : MonoBehaviour
{
    public float prize = 1.0f;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "CarBody")
        {
            NNCarController car = col.transform.root.GetComponent<NNCarController>();
            car.AwardVictoryPoints(prize);

            this.gameObject.SetActive(false);
        }
    }
}
