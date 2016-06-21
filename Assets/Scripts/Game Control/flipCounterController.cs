using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class flipCounterController : MonoBehaviour
{
    public Text countdownText;
    public AnimationCurve textSizeAnimation;

    private GameObject currentlyDisplayedObject;

    private float textSizeTime;

    public void TriggerFlipTimeout(GameObject sender, Action callback)
    {
        Rigidbody body = sender.transform.root.GetComponent<Rigidbody>();
        if (body == null)
            return;

        StartCoroutine(UnflipBody(body, callback));
    }

    private IEnumerator UnflipBody(Rigidbody body, Action callback)
    {
        float startTime = Time.time;

        while(BodyUneven(body))
        {
            body.GetComponent<PlayerHealth>().enabled = false;

            body.AddForce(Vector3.up * (4.0f - body.position.y) * 0.1f, ForceMode.VelocityChange);

            body.transform.rotation = Quaternion.Lerp(
                body.transform.rotation,
                Quaternion.identity,
                (Time.time - startTime) * 0.005f
                );

            yield return new WaitForFixedUpdate();
        }

        // Finished, call the callback
        callback();
        body.GetComponent<PlayerHealth>().enabled = true;
        body.isKinematic = false;
    }

    private bool BodyUneven(Rigidbody body)
    {
        return
            body.angularVelocity.magnitude > 0.2f
            || (Vector3.Dot(body.transform.up, Vector3.up) < 0.98f);
    }

    public void UpdateCountdown(GameObject sender, int timerVal)
    {
        // Do not update if we're currently showing the player's time
        if (currentlyDisplayedObject != null
            && currentlyDisplayedObject != sender
            && currentlyDisplayedObject.transform.root.tag == "Player")
            return;

        if (countdownText != null)
        {
            countdownText.text = timerVal.ToString();
            textSizeTime = 0.0f;
        }

        currentlyDisplayedObject = sender;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (countdownText == null)
            return;

        if (textSizeTime < textSizeAnimation.keys[textSizeAnimation.length - 1].time)
        {
            countdownText.transform.localScale = Vector3.one * textSizeAnimation.Evaluate(textSizeTime);
            textSizeTime += Time.deltaTime;
        }
        else
        {
            countdownText.text = "";
            currentlyDisplayedObject = null;
            textSizeTime = 0.0f;
        }
	}
}
