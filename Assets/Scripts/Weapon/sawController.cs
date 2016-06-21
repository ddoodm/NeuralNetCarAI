using UnityEngine;
using System.Collections;

public class sawController : MonoBehaviour, Weapon {

    private float animationTime = 1;
    public AnimationCurve activeCurve;
    public AnimationCurve lower;
    public AnimationCurve raise;

    public bool canLower = true, canRaise = false, raisePending = false;

    public Vector3 initialRot;

    public GameObject movingPart;

    // Update is called once per frame
    void Update()
    {
        if(movingPart)
            DoAnimation();
    }

    private void DoAnimation()
    {
        animationTime += Time.deltaTime;
        float animCurveValue = activeCurve.Evaluate(animationTime);
        if(movingPart != null)
            movingPart.transform.rotation = Quaternion.Euler(new Vector3(0, transform.root.rotation.eulerAngles.y - initialRot.y, -90 + animCurveValue * 90));

        if (animationTime >= 1.0f && activeCurve == raise)
            canLower = true;

        if (animationTime >= 1.0f && activeCurve == lower)
            canRaise = true;

        // Try raise
        if (canRaise && raisePending)
        {
            animationTime = 0;
            activeCurve = raise;
            movingPart.GetComponentInChildren<sawSpinner>().spinSpeed = -1;
            raisePending = false;
            canLower = false;
            canRaise = false;
        }
    }

    public void Use()
    {
        // Do not access the moving part if it is gone
        if (!movingPart)
            return;

        if (canLower)
        {
            animationTime = 0;
            activeCurve = lower;
            movingPart.GetComponentInChildren<sawSpinner>().spinSpeed = -32;
            canLower = false;
            canRaise = false;
            raisePending = false;
        }
    }

    public void EndUse()
    {
        // Do not access the moving part if it is gone
        if (!movingPart)
            return;

        raisePending = true;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
