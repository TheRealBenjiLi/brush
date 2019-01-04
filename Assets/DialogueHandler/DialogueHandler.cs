using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {

	public Canvas myCanvas;
	public Text speaker;
	public Text dialogue;
	public Text continueEnd;

	private bool isActive;

	public void Start () {
		isActive = false;
	}

	public void Update () {
		gameObject.SetActive(isActive);
		if (isActive) {
			if (Input.GetButtonDown("Fire1")) {
				isActive = false;
			}
		}
	}

	public void Speak (string newspeaker, string newdialogue) {
		isActive = true;
		gameObject.SetActive(isActive);
		speaker.text = newspeaker;
		dialogue.text = newdialogue;
	}
}
