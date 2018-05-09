using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	public void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Player") {
			Player playerScript = col.gameObject.GetComponent<Player>();
			playerScript.TakeDamage(1);
		}
		Destroy(gameObject);
	}
}
