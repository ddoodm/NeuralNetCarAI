using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShadowTextUpdater : MonoBehaviour
{
    private Text parentText, thisText;

    void Start ()
    {
        parentText = transform.parent.GetComponent<Text>();
        if (parentText == null)
            throw new Exception("Shadow-Text updaters require the parent to have a Text element.");

        thisText = this.GetComponent<Text>();
    }

	void Update ()
    {
        thisText.text = parentText.text;
        thisText.fontSize = parentText.fontSize;
	}
}
