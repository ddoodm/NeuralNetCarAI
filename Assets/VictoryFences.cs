using UnityEngine;
using System.Collections;

public class VictoryFences : MonoBehaviour
{
    public void Reset()
    {
        foreach (Transform t in this.transform)
            t.gameObject.SetActive(true);
    }
}
