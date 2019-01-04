using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	// Set this in the Unity editor
	public int timeLimit;
	public string targetTag;

	// This traces a hitbox back to its creator
	public Entity creator;
	public int characterid;
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

	public void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == targetTag) {
			Entity script = col.gameObject.GetComponent<Entity>();
			script.TakeDamage(damage);
			if (characterid == 1) {
				SwordGuyScript p = (SwordGuyScript) creator;
				p.OnHitSuccess();
			} else {

			}
		}
		Destroy(gameObject);
	}
}
