using UnityEngine;
using System.Collections;

public class weaponStats : MonoBehaviour {

    public float mass, damageMultiplier, hp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        checkDestroy();
	
	}

    void checkDestroy()
    {

        if (hp <= 0)
        {
            if (transform.parent.parent.tag != null)
            {
                switch (transform.parent.parent.tag)
                {
                    case "Socket_Left":
                        transform.root.GetComponent<SocketEquipment>().equipmentRefs[0] = null;
                        break;
                    case "Socket_Right":
                        transform.root.GetComponent<SocketEquipment>().equipmentRefs[1] = null;
                        break;
                    case "Socket_Front":
                        transform.root.GetComponent<SocketEquipment>().equipmentRefs[2] = null;
                        break;
                    case "Socket_Back":
                        transform.root.GetComponent<SocketEquipment>().equipmentRefs[3] = null;
                        break;
                    case "Socket_Top":
                        transform.root.GetComponent<SocketEquipment>().equipmentRefs[4] = null;
                        break;
                    default:
                        break;
                }
            }
            Destroy(transform.parent.gameObject);
        }
    }

    public void issueDamageAttachment(float damage)
    {
        float attachmentMultiplier = Mathf.Abs(1 - damageMultiplier);
        if (damageMultiplier < 1)
            attachmentMultiplier += 1;
        

        /*
        Debug.Log("Damage before multiplier: " + damage);
        Debug.Log("Damage multiplier: " + attachmentMultiplier);
        Debug.Log(gameObject.name + " hit for " + damage * attachmentMultiplier);
        Debug.Log("HP before hit " + hp);
        */
        hp -= damage * attachmentMultiplier;
        
        //Debug.Log("Remaining hp " + hp);
    }
}
