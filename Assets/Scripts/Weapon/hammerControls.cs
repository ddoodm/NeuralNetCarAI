using UnityEngine;
using System.Collections;

public class hammerControls : MonoBehaviour {

    private float animationTime = 1;
    public AnimationCurve curve;

    public bool canFlip = true;

    public Vector3 initialRot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        DoAnimation();
	
	}

    public void useHammer()
    {
        if (canFlip)
        {
            animationTime = 0;
            canFlip = false;
        }
    }

    private void DoAnimation()
    {
        animationTime += Time.deltaTime;
        float animCurveValue = curve.Evaluate(animationTime);
        this.transform.rotation = Quaternion.Euler(new Vector3(-90 -animCurveValue * 90, transform.root.rotation.eulerAngles.y - initialRot.y, 0));

        // The player may flip once the animation is reset
        if (animationTime >= 1.0f)
            canFlip = true;
    }
}
