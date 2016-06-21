using UnityEngine;
using System.Collections;

public class DebugEquipmentAttachment : MonoBehaviour
{
    public Equipment[] equipment;

	// Use this for initialization
	void Start () {
        // Jesse's code needs the mower to be rotated 0'. lol.
        Quaternion oldRotation = this.transform.rotation;
        this.transform.rotation = Quaternion.identity;
        // more retarded code from jesse (sorry)
        GetComponent<SocketEquipment>().SocketItems(equipment, "BaseMower");
        this.transform.rotation = oldRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
