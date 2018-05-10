using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	// Set this in the Unity editor
	public int timeLimit;
	public string targetTag;
	public int damage;

	private int t;

	public void Start () {
		t = 0;
	}

	public void Update () {
		if (t > timeLimit) {
			Destroy(gameObject);
		}
		t++;
	}

	public void SetTimeLimit (int newTimeLimit) {
		timeLimit = newTimeLimit;
	}

	public void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == targetTag) {
			Entity script = col.gameObject.GetComponent<Entity>();
			script.TakeDamage(damage);
		}
		Destroy(gameObject);
	}
}
