using UnityEngine;
using System.Collections;

public class LightningCreator : MonoBehaviour {

    public Lightning lightningPrefab;
    public bool electric;
    public GameObject opponent;
    public int zapRange;
    
    IEnumerator Zap() {
        while(electric)
        {
            Debug.Log("zapping");
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);

            yield return null;
        }
	}

    public void Use()
    {
        if (Vector3.Distance(this.transform.position, opponent.transform.position) < zapRange)
        {
            electric = true;
        }
        else
        {
            electric = false;
        }
    }

    void Start()
    {
        switch (transform.root.tag)
        {
            case "Player":
                opponent = GameObject.FindGameObjectWithTag("Enemy");
                break;
            case "Enemy":
                opponent = GameObject.FindGameObjectWithTag("Player");
                break;
        }
        StartCoroutine(Zap());
    }

    void Update()
    {
        if (opponent != null)
        {
            if (Vector3.Distance(this.transform.position, opponent.transform.position) > zapRange)
            {
                electric = false;
            }
        }
        if (electric)
        {
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);
        }
    }

    public void endUse()
    {
        electric = false;
    }


}
