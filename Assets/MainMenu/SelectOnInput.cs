using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectObject;

	private bool selected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") != 0 && selected == false) {
			eventSystem.SetSelectedGameObject(selectObject);
			selected = true;
		}
	}

	private void OnDisable () {
		selected = false;
	}
}
