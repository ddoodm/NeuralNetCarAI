using UnityEngine;
using System.Collections;

public class lightningController : MonoBehaviour, Weapon {

    private LightningCreator zap;
    public float lightningDamage;

    private GameObject opponent;
	// Use this for initialization
	void Start () {
        zap = transform.GetComponentInChildren<LightningCreator>();
        switch (transform.root.tag)
        {
            case "Player":
                opponent = GameObject.FindGameObjectWithTag("Enemy");
                break;
            case "Enemy":
                opponent = GameObject.FindGameObjectWithTag("Player");
                break;
        }

	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

    public void Use()
    {
        EnergyController energyCtrl = transform.root.GetComponent<EnergyController>();
        Debug.Log("is energy full? " + energyCtrl.energyFull);
        Debug.Log("energy amount " + energyCtrl.energy);
        if (zap != null && energyCtrl.energyFull)
        {
            // Do not access the moving part if it is gone
            zap.Use();
            opponent.GetComponent<PlayerHealth>().health -= lightningDamage;
            energyCtrl.energy -= energyCtrl.maxEnergy;
            
        }
    }

    public void EndUse()
    {
        if (zap != null)
        {
            zap.endUse();
        }
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
