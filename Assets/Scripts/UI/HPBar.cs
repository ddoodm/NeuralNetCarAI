using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour
{
    public float maxValue = 1.0f;
    private float realScale = 1.0f;

    public HPBar()
    {
        // This is a canvas child, so it has a local scale of 1
        //realScale = this.transform.localScale.x;
    }

    public float value
    {
        get { return this.transform.localScale.x; }
        set
        {
            if (value == float.PositiveInfinity)
                return;

            this.transform.localScale =
                new Vector3(value / maxValue * realScale,
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
    }
}
